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
        IRefIdProperty RefIdProperty { get; }
        IRefEntityProperty RefEntityProperty { get; }
    }

    /// <summary>
    /// 引用属性声明
    /// </summary>
    /// <typeparam name="T">属性值类型</typeparam>
    public interface IRefProperty<T> : IRefProperty
    {

    }
}
