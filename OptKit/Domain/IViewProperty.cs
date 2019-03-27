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
        /// <summary>
        /// 视图路径
        /// </summary>
        string ViewPath { get; }
    }

    /// <summary>
    /// 视图属性声明
    /// </summary>
    /// <typeparam name="T">属性值类型</typeparam>
    public interface IViewProperty<T> : IProperty<T>, IViewProperty
    {

    }

    class ViewProperty : Property, IViewProperty
    {
        public string ViewPath { get; set; }
    }

    class ViewProperty<T> : ViewProperty, IViewProperty<T>
    {
    }
}
