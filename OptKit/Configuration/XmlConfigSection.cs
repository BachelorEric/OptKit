using OptKit.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using OptKit.Serialization;

namespace OptKit.Configuration
{
    /// <summary>
    /// A container for settings - key/value pairs where keys are strings, and values are arbitrary objects.
    /// Instances of this class are thread-safe.
    /// </summary>
    public class XmlConfigSection : ConfigSection
    {
        /// <summary>
        /// 构造实例
        /// </summary>
        public XmlConfigSection() { }
        /// <summary>
        /// 构造实例
        /// </summary>
        /// <param name="parent"></param>
        protected XmlConfigSection(XmlConfigSection parent) : base(parent) { }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        protected override object Serialize<T>(object value, string key)
        {
            if (value == null)
                return null;
            TypeConverter c = TypeDescriptor.GetConverter(typeof(T));
            if (c != null && c.CanConvertTo(typeof(string)) && c.CanConvertFrom(typeof(string)))
            {
                return c.ConvertToInvariantString(value);
            }

            var element = new XElement("Object");
            if (key != null)
            {
                element.Add(new XAttribute("key", key));
            }
            element.Value = XmlConvert.Serialize(value);
            return element;
        }

        protected override object SerializeList<T>(T[] array, string key)
        {
            object[] serializedArray = new object[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                serializedArray[i] = Serialize<T>(array[i], null);
            }
            return serializedArray;
        }

        protected override T Deserialize<T>(object serializedVal)
        {
            if (serializedVal == null)
                return default(T);
            Type targetType = typeof(T);
            XElement element = serializedVal as XElement;
            if (element != null)
            {
                return XmlConvert.Deserialize<T>(element.Value);
            }
            else
            {
                string text = serializedVal as string;
                if (text == null)
                    throw new InvalidOperationException("Cannot read a section container as a single value");
                TypeConverter c = TypeDescriptor.GetConverter(targetType);
                return (T)c.ConvertFromInvariantString(text);
            }
        }

        protected override IReadOnlyList<T> DeserializeList<T>(object value, string key)
        {
            object[] serializedArray = value as object[];
            if (serializedArray != null)
            {
                try
                {
                    T[] array = new T[serializedArray.Length];
                    for (int i = 0; i < array.Length; i++)
                    {
                        array[i] = Deserialize<T>(serializedArray[i]);
                    }
                    return array;
                }
                catch (NotSupportedException ex)
                {
                    RT.Logger.Warn(ex);
                }
            }
            else
            {
                RT.Logger.Warn("XmlConfigSection.GetList(" + key + ") - this entry is not a list");
            }
            return new T[0];
        }

        protected override ConfigSection Create(ConfigSection parent)
        {
            return parent != null ? new XmlConfigSection(parent as XmlConfigSection) : new XmlConfigSection();
        }

        public static XmlConfigSection Load(FileName fileName)
        {
            try
            {
                return Load(XDocument.Load(fileName).Root);
            }
            catch (Exception exc)
            {
                RT.Logger.Warn(exc);
                var section = new XmlConfigSection();
                section.Save(fileName);
                return section;
            }
        }

        public static XmlConfigSection Load(XElement element)
        {
            XmlConfigSection section = new XmlConfigSection();
            section.LoadContents(element.Elements());
            return section;
        }

        internal void LoadContents(IEnumerable<XElement> elements)
        {
            foreach (var element in elements)
            {
                string key = (string)element.Attribute("key");
                if (key == null)
                    continue;
                switch (element.Name.LocalName)
                {
                    case "Section":
                        dict[key] = element.Value;
                        break;
                    case "Array":
                        dict[key] = LoadArray(element.Elements());
                        break;
                    case "Object":
                        dict[key] = new XElement(element);
                        break;
                    case "Sections":
                        XmlConfigSection child = new XmlConfigSection(this);
                        child.LoadContents(element.Elements());
                        dict[key] = child;
                        break;
                }
            }
        }

        static object[] LoadArray(IEnumerable<XElement> elements)
        {
            List<object> result = new List<object>();
            foreach (var element in elements)
            {
                switch (element.Name.LocalName)
                {
                    case "Null":
                        result.Add(null);
                        break;
                    case "Element":
                        result.Add(element.Value);
                        break;
                    case "Object":
                        result.Add(new XElement(element));
                        break;
                }
            }
            return result.ToArray();
        }

        public override void Save(FileName fileName)
        {
            new XDocument(Save()).Save(fileName);
        }

        public XElement Save()
        {
            lock (syncRoot)
            {
                return new XElement("Sections", SaveContents());
            }
        }

        IReadOnlyList<XElement> SaveContents()
        {
            List<XElement> result = new List<XElement>();
            foreach (var pair in dict)
            {
                XAttribute key = new XAttribute("key", pair.Key);
                XmlConfigSection child = pair.Value as XmlConfigSection;
                if (child != null)
                {
                    var contents = child.SaveContents();
                    if (contents.Count > 0)
                        result.Add(new XElement("Sections", key, contents));
                }
                else if (pair.Value is object[])
                {
                    object[] array = (object[])pair.Value;
                    XElement[] elements = new XElement[array.Length];
                    for (int i = 0; i < array.Length; i++)
                    {
                        XElement obj = array[i] as XElement;
                        if (obj != null)
                        {
                            elements[i] = new XElement(obj);
                        }
                        else if (array[i] == null)
                        {
                            elements[i] = new XElement("Null");
                        }
                        else
                        {
                            elements[i] = new XElement("Element", (string)array[i]);
                        }
                    }
                    result.Add(new XElement("Array", key, elements));
                }
                else if (pair.Value is XElement)
                {
                    result.Add(new XElement((XElement)pair.Value));
                }
                else
                {
                    result.Add(new XElement("Section", key, (string)pair.Value));
                }
            }
            return result;
        }
    }
}
