using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Collections.Specialized;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Reflection;
using OptKit;

namespace System
{
    /// <summary>
    /// 常用扩展
    /// </summary>
    public static class SystemExtension
    {
        /// <summary>
        /// 把对象转为指定的类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="obj"></param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <param name="ignoreException">转换失败时是否忽略异常，默认为true</param>
        /// <returns>转换成目标类型的对象</returns>
        public static T ConvertTo<T>(this object obj, T defaultValue, bool ignoreException = true)
        {
            if (ignoreException)
            {
                if (obj == null)
                    return defaultValue;
                try
                {
                    object result;
                    if (TryConventTo(obj, typeof(T), out result))
                        return (T)result;
                }
                catch { }
                return defaultValue;
            }
            return ConvertTo<T>(obj);
        }

        static object ConvertTo(JToken jToken, Type type)
        {
            if (jToken is JObject)
            {
                var jObject = (JObject)jToken;
                JToken jtype = null;
                if (jObject.TryGetValue("$type", StringComparison.OrdinalIgnoreCase, out jtype))
                {
                    var declareType = Type.GetType((jtype as JValue).Value?.ToString());
                    var newReader = jObject.Root.CreateReader();
                    if (declareType != null)
                    {
                        type = declareType;
                    }
                }
            }
            return JsonSerializer.Create(JsonConvert.DefaultSettings?.Invoke()).Deserialize(jToken.CreateReader(), type);
        }

        /// <summary>
        /// 转换为指定类型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>转换成目标类型的对象</returns>
        public static object ConvertTo(this object obj, Type targetType)
        {
            if (obj != null)
            {
                object result;
                if (TryConventTo(obj, targetType, out result))
                    return result;

                throw new InvalidOperationException(OptKit.Properties.Resources.CannotConvertType.FormatArgs(obj.GetType().Name, targetType.Name));
            }
            return targetType.GetDefault();
        }

        static bool TryConventTo(object obj, Type targetType, out object result)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (obj is JToken)
            {
                result = ConvertTo((JToken)obj, targetType);
                return true;
            }
            if (obj is DBNull)
            {
                result = targetType.GetDefault();
                return true;
            }

            var sourceType = obj.GetType();
            if (targetType.IsAssignableFrom(sourceType))
            {
                result = obj;
                return true;
            }

            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
                targetType = Nullable.GetUnderlyingType(targetType);

            if (targetType.IsEnum)
            {
                result = Enum.Parse(targetType, obj.ToString(), true);
                return true;
            }

            if (targetType == typeof(bool))//对bool特殊处理
            {
                if ("1".Equals(obj))
                {
                    result = true;
                    return true;
                }
                if ("0".Equals(obj))
                {
                    result = false;
                    return true;
                }
            }
            //处理数字类型。（空字符串转换为数字 0）
            if ((targetType.IsPrimitive || targetType == typeof(decimal)) &&
                obj is string && string.IsNullOrEmpty(obj as string))
            {
                result = 0;
                return true;
            }

            if (typeof(IConvertible).IsAssignableFrom(sourceType) &&
                typeof(IConvertible).IsAssignableFrom(targetType))
            {
                result = Convert.ChangeType(obj, targetType);
                return true;
            }

            var converter = System.ComponentModel.TypeDescriptor.GetConverter(obj);
            if (converter != null && converter.CanConvertTo(targetType))
            {
                result = converter.ConvertTo(obj, targetType);
                return true;
            }

            converter = System.ComponentModel.TypeDescriptor.GetConverter(targetType);
            if (converter != null && converter.CanConvertFrom(sourceType))
            {
                result = converter.ConvertFrom(obj);
                return true;
            }

            try
            {
                result = Convert.ChangeType(obj, targetType);
                return true;
            }
            catch { }

            result = null;
            return false;
        }

        /// <summary>
        /// 把对象转换为指定类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="obj">原对象</param>
        /// <returns>转换成目标类型的对象</returns>
        public static T ConvertTo<T>(this object obj)
        {
            if (obj is T)
                return (T)obj;
            return (T)ConvertTo(obj, typeof(T));
        }

        /// <summary>
        /// 返回输出SQL字符串，把命令的参数也格式化输出
        /// </summary>
        /// <param name="cmd">Db命令</param>
        /// <returns></returns>
        public static string ToTraceString(this IDbCommand cmd)
        {
            var content = cmd.CommandText;

            if (cmd.Parameters.Count > 0)
            {
                var pValues = cmd.Parameters.OfType<DbParameter>().Select(p =>
                {
                    var value = p.Value;
                    if (value is string)
                    {
                        value = '"' + value.ToString() + '"';
                    }
                    return p.ParameterName + "->" + value;
                });
                content += Environment.NewLine + pValues.Join(",");
            }
            return content;
        }

        /// <summary>
        /// 获取值，如果不存在指定键，返回T的默认值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="hd">字典</param>
        /// <param name="key">键</param>
        /// <param name="defaultValue">字典找不到值，或者值类型转换失败时返回的默认值</param>
        /// <returns></returns>
        public static T GetValue<T>(this HybridDictionary hd, object key, T defaultValue = default(T))
        {
            if (hd.Contains(key))
                return hd[key].ConvertTo<T>(defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 获取元素值
        /// </summary>
        /// <param name="element">元素</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetElementValue(this XElement element, string name)
        {
            return element?.Element(name).Value;
        }

        /// <summary>
        /// 获取枚举上的标签
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToCaption(this Enum value)
        {
            if (value == null)
                return null;
            var type = value.GetType();
            var fieldInfo = type.GetField(value.ToString());
            return fieldInfo?.GetCustomAttribute<CaptionAttribute>()?.Caption;
        }
    }
}