using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain
{
    /// <summary>
    /// 引用实体属性声明
    /// </summary>
    public interface IRefEntityProperty : IRefProperty
    {
    }

    /// <summary>
    /// 引用实体属性声明
    /// </summary>
    /// <typeparam name="T">属性值类型</typeparam>
    public interface IRefEntityProperty<T> : IRefEntityProperty
    {

    }
}
