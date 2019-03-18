using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using OptKit.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OptKit.Configuration
{
    /// <summary>
    /// A container for settings - key/value pairs where keys are strings, and values are arbitrary objects.
    /// Instances of this class are thread-safe.
    /// </summary>
    public class JsonConfigSection : ConfigSection
    {
        public JsonConfigSection()
        {
        }

        protected JsonConfigSection(JsonConfigSection parent):base(parent)
        {
        }

        protected override ConfigSection Create(ConfigSection parent)
        {
            return parent != null ? new JsonConfigSection(parent as JsonConfigSection) : new JsonConfigSection();
        }

        protected override object Serialize<T>(object value, string key)
        {
            return JToken.FromObject(value);
        }

        protected override object SerializeList<T>(T[] value, string key)
        {
            return JToken.FromObject(value);
        }

        protected override T Deserialize<T>(object serializedVal)
        {
            return serializedVal.ConvertTo<T>();
        }

        protected override IReadOnlyList<T> DeserializeList<T>(object value, string key)
        {
            var serializedArray = value as JArray;
            if (serializedArray != null)
            {
                try
                {
                    List<T> list = new List<T>();
                    foreach (var element in serializedArray.Values())
                    {
                        list.Add(element.ConvertTo<T>());
                    }
                    return list;
                }
                catch (NotSupportedException ex)
                {
                    RT.Logger.Warn(ex);
                }
            }
            else
            {
                RT.Logger.Warn("JsonConfigSection.GetList(" + key + ") - this entry is not a list");
            }
            return new T[0];
        }

        public static JsonConfigSection Load(FileName fileName)
        {
            try
            {
                using (var sr = new StreamReader(fileName))
                {
                    var json = sr.ReadToEnd();
                    return Load(json);
                }
            }
            catch (Exception exc)
            {
                RT.Logger.Warn(exc);
                var section = new JsonConfigSection();
                section.Save(fileName);
                return section;
            }
        }

        public static JsonConfigSection Load(string json)
        {
            JsonConfigSection section = new JsonConfigSection();
            var obj = JObject.Parse(json);
            section.Load(obj);
            return section;
        }


        internal void Load(JObject obj)
        {
            foreach (var p in obj.Properties())
            {
                if (p.Name == "_sections")
                {
                    var sections = (JObject)p.Value;
                    foreach (var child in sections.Properties())
                    {
                        JsonConfigSection section = new JsonConfigSection();
                        section.Load((JObject)child.Value);
                        dict[child.Name] = section;
                    }
                }
                else
                {
                    dict[p.Name] = p.Value;
                }
            }
        }

        public override void Save(FileName fileName)
        {
            using (var sw = new StreamWriter(fileName))
            {
                sw.Write(Save());
            }
        }

        public string Save()
        {
            lock (syncRoot)
            {
                var content = SaveContents();
                return content.ToString();
            }
        }

        string GetValue(JToken token)
        {
            if (token.Type == JTokenType.String)
                return "\"{0}\"".FormatArgs(token);
            return token.ToString();
        }

        JObject SaveContents()
        {
            JObject obj = new JObject();
            Dictionary<string, JsonConfigSection> children = new Dictionary<string, JsonConfigSection>();
            foreach (var pair in dict)
            {
                JsonConfigSection child = pair.Value as JsonConfigSection;
                if (child != null)
                {
                    children.Add(pair.Key, child);
                }
                else
                {
                    JToken token = pair.Value as JToken ?? JToken.FromObject(pair.Value);
                    obj.Add(pair.Key, token);
                }
            }
            if (children.Any())
            {
                JObject section = new JObject();
                foreach (var child in children)
                {
                    section.Add(child.Key, child.Value.SaveContents());
                }
                obj.Add("_sections", section);
            }
            return obj;
        }
    }
}
