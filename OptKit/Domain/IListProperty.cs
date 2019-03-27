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
        Type ListType { get; }
    }

    /// <summary>
    /// 列表属性声明
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IListProperty<T> : IProperty<T>, IListProperty
    {

    }

    class ListProperty<T> : Property, IListProperty<T>
    {
        public Type ListType => throw new NotImplementedException();
    }
}
