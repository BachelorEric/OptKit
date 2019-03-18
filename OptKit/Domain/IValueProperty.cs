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
    public interface IValueProperty<T> : IValueProperty
    {
    }

    public interface IValuePropertyMeta
    {
        object MaxValue { get; }

        object MinValue { get; }

        bool Updatable { get; }

        DisplayFormatAttribute DisplayFormat { get; set; }

        DisplayColumnAttribute DisplayColumn { get; }

        ValidationAttribute[] Attributes { get; }
    }
}
