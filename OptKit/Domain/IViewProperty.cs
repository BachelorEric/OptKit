using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain
{
    /// <summary>
    /// 视图属性声明
    /// </summary>
    public interface IViewProperty : IProperty
    {
    }

    /// <summary>
    /// 视图属性声明
    /// </summary>
    /// <typeparam name="T">属性值类型</typeparam>
    public interface IViewProperty<T> : IViewProperty
    {

    }
}
