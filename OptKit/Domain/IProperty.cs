using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain
{
    /// <summary>
    /// 属性声明
    /// </summary>
    public interface IProperty
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        string PropertyName { get;  }
        /// <summary>
        /// 属性值类型
        /// </summary>
        Type PropertyType { get;  }
        /// <summary>
        /// 属性所有者类型
        /// </summary>
        Type OwnerType { get;  }
        /// <summary>
        /// 属性声明类型
        /// </summary>
        Type DeclareType { get;  }

        IPropertyMeta Meta { get; }
    }

    /// <summary>
    /// 属性声明
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IProperty<T> : IProperty
    {

    }

    public interface IPropertyMeta
    {

    }
}
