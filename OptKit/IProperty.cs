using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit
{
    /// <summary>
    /// 属性声明
    /// </summary>
    public interface IProperty
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        string PropertyName { get; }
        /// <summary>
        /// 属性值类型
        /// </summary>
        Type PropertyType { get; }
        /// <summary>
        /// 属性所有者类型
        /// </summary>
        Type OwnerType { get; }
        /// <summary>
        /// 属性声明类型
        /// </summary>
        Type DeclareType { get; }
        /// <summary>
        /// 编译索引
        /// </summary>
        int CompiledIndex { get; }
    }

    /// <summary>
    /// 属性声明
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IProperty<T> : IProperty
    {

    }

    public abstract class Property : IProperty
    {
        public string PropertyName { get; internal set; }

        public Type PropertyType { get; internal set; }

        public Type OwnerType { get; internal set; }

        public Type DeclareType { get; internal set; }

        internal int GlobalIndex { get; set; } = -1;

        public int CompiledIndex { get; internal set; } = -1;
    }
}
