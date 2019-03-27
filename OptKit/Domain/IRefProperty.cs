using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain
{
    /// <summary>
    /// 引用属性声明
    /// </summary>
    public interface IRefProperty : IProperty
    {
        /// <summary>
        /// 引用实体的类型
        /// </summary>
        Type RefEntityType { get; }

        /// <summary>
        /// 实体引用的类型
        /// </summary>
        ReferenceType ReferenceType { get; }

        /// <summary>
        /// 该引用属性是否可空。
        /// 如果引用Id属性的类型是引用类型（字符串）或者是一个 Nullable 类型，则这个属性返回 true。
        /// </summary>
        bool Nullable { get; }

        /// <summary>
        /// 返回对应的引用 Id 属性。
        /// </summary>
        IRefIdProperty RefIdProperty { get; }

        /// <summary>
        /// 返回对应的引用实体属性。
        /// </summary>
        IRefEntityProperty RefEntityProperty { get; }
    }

    /// <summary>
    /// 引用属性声明
    /// </summary>
    /// <typeparam name="T">属性值类型</typeparam>
    public interface IRefProperty<T> : IProperty<T>, IRefProperty
    {

    }

    class RefProperty<T> : Property, IRefProperty<T>
    {
        public Type RefEntityType { get; set; }

        public ReferenceType ReferenceType { get; set; }

        public bool Nullable { get; set; }

        public IRefIdProperty RefIdProperty { get; set; }

        public IRefEntityProperty RefEntityProperty { get; set; }
    }
}
