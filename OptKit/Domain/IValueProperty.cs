using OptKit.Domain.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OptKit.Domain
{
    /// <summary>
    /// 值属性声明
    /// </summary>
    public interface IValueProperty : IProperty
    {
    }

    /// <summary>
    /// 值属性声明
    /// </summary>
    /// <typeparam name="T">属性值类型</typeparam>
    public interface IValueProperty<T> : IProperty<T>, IValueProperty
    {
    }

    class ValueProperty : Property, IValueProperty
    {

    }

    class ValueProperty<T> : ValueProperty, IValueProperty<T>
    {

    }
}
