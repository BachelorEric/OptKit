using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;

namespace System
{
    /// <summary>
    /// 类型方法扩展
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// 获取继承层次列表，从子类到基类
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="exceptTypes">The except types.</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetHierarchy(this Type from, params Type[] exceptTypes)
        {
            var needExcept = exceptTypes.Length > 0;

            Type current = from;
            while (current != null && (!needExcept || !InExcept(current, exceptTypes)))
            {
                yield return current;
                current = current.BaseType;
            }
        }

        static bool InExcept(Type current, Type[] exceptTypes)
        {
            for (int i = 0, c = exceptTypes.Length; i < c; i++)
            {
                var exceptType = exceptTypes[i];

                //如果是泛型定义，则需要 current 类型是这个泛型的实例也可以。
                if (exceptType.IsGenericTypeDefinition)
                {
                    if (current.IsGenericType && current.GetGenericTypeDefinition() == exceptType) return true;
                }
                else
                {
                    if (exceptType == current) return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 判断指定的类型是否是一个指定的泛型类型定义。
        /// </summary>
        /// <param name="targetType">需要判断的目标类型。</param>
        /// <param name="genericType">泛型类型定义</param>
        /// <returns></returns>
        public static bool IsGenericType(this Type targetType, Type genericType)
        {
            return targetType.IsGenericType && targetType.GetGenericTypeDefinition() == genericType;
        }

        /// <summary>
        /// 判断类型是否标记<see cref="SerializableAttribute"/>
        /// </summary>
        /// <param name="objectType">Type to check.</param>
        /// <returns>True if the type is Serializable.</returns>
        public static bool IsSerializable(this Type objectType)
        {
            var result = objectType.GetCustomAttributes(typeof(SerializableAttribute), false);
            return (result != null && result.Length > 0);
        }

        /// <summary>
        /// 获取指定的泛型定义的类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="genericTypeDefinition"></param>
        /// <returns></returns>
        public static Type GetGenericType(this Type type, Type genericTypeDefinition)
        {
            if (!genericTypeDefinition.IsGenericTypeDefinition)
                throw new ArgumentException(OptKit.Properties.Resources.MustBeGenericTypeDefinition.FormatArgs(genericTypeDefinition.Name));
            var currentType = type;
            while (currentType != null && currentType != typeof(object))
            {
                if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == genericTypeDefinition)
                    return currentType;
                currentType = currentType.BaseType;
            }
            return null;
        }

        /// <summary>
        /// 获取类型的默认值
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object GetDefault(this Type t)
        {
            if (t.IsClass) return null;
            Func<object> f = GetDefault<object>;
            return f.Method.GetGenericMethodDefinition().MakeGenericMethod(t).Invoke(null, null);
        }

        static T GetDefault<T>()
        {
            return default(T);
        }

        /// <summary>
        /// 如果是 Nullable 泛型类型，则返回内部的真实类型。
        /// </summary>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public static Type IgnoreNullable(this Type targetType)
        {
            if (IsNullable(targetType)) { return targetType.GetGenericArguments()[0]; }

            return targetType;
        }

        /// <summary>
        /// 判断某个类型是否为 Nullable 泛型类型。
        /// </summary>
        /// <param name="targetType">需要判断的目标类型。</param>
        /// <returns></returns>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static bool IsNullable(this Type targetType)
        {
            return targetType.IsGenericType && !targetType.IsGenericTypeDefinition && targetType.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// 获取 System.Type 的程序集限定名，其中包括从中加载 System.Type 的程序集的名称(不带版本)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetQualifiedName(this Type type)
        {
            return type.FullName + "," + type.Assembly.GetName().Name;
        }
    }
}