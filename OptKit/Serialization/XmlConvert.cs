using System;
using System.IO;
using System.Xml.Serialization;

namespace OptKit.Serialization
{
    /// <summary>
    /// Xml转换
    /// </summary>
    public class XmlConvert
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static string Serialize(object graph)
        {
            var xmlFormatter = new XmlSerializer(graph.GetType());
            StringWriter w = new StringWriter();
            xmlFormatter.Serialize(w, graph);
            return w.ToString();
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static object Deserialize(Type type, string xml)
        {
            var xmlFormatter = new XmlSerializer(type);
            StringReader sr = new StringReader(xml);
            return xmlFormatter.Deserialize(sr);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string xml)
        {
            return (T)Deserialize(typeof(T), xml);
        }
    }
}
