using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain
{
    /// <summary>
    /// 列表属性声明
    /// </summary>
    public interface IListProperty : IProperty
    {
    }

    /// <summary>
    /// 列表属性声明
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IListProperty<T> : IListProperty
    {

    }
}
