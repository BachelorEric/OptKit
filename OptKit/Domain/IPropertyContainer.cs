using OptKit.Properties;
using OptKit.Reflection;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain
{
    /// <summary>
    /// 属性容器声明
    /// </summary>
    public interface IPropertyContainer
    {
        /// <summary>
        /// 属性所有者
        /// </summary>
        Type OwnerType { get; }

        /// <summary>
        /// 属性列表
        /// </summary>
        IReadOnlyList<IProperty> Properties { get; }

        /// <summary>
        /// 查找属性，找不到返回 null 值
        /// </summary>
        /// <param name="proprtyName">属性名称</param>
        /// <param name="ignoreCase">是否忽略属性名称大小写</param>
        /// <returns></returns>
        IProperty Find(string proprtyName, bool ignoreCase = false);
    }

    /// <summary>
    /// 属性容器扩展
    /// </summary>
    public static class PropertyContainerExtension
    {
        /// <summary>
        /// 获取属性，找不到会引起<see cref="PropertyNotFoundException"/>异常
        /// </summary>
        /// <param name="container">属性容器</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="ignoreCase">是否忽略属性名称大小写</param>
        /// <returns></returns>
        public static IProperty Get(this IPropertyContainer container, string propertyName, bool ignoreCase = false)
        {
            var property = container.Find(propertyName, ignoreCase);
            if (property == null)
                throw new PropertyNotFoundException(Resources.TypePropertyNotFound.FormatArgs(propertyName, container.OwnerType.GetQualifiedName()));
            return property;
        }
    }
}
