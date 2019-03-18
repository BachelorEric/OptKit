using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain
{
    /// <summary>
    /// 引用ID属性声明
    /// </summary>
    public interface IRefIdProperty : IRefProperty
    {
    }

    /// <summary>
    /// 引用ID属性声明
    /// </summary>
    /// <typeparam name="T">属性值类型</typeparam>
    public interface IRefIdProperty<T> : IRefIdProperty
    {

    }
}
