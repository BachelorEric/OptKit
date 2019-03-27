using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain
{
    /// <summary>
    /// 计算属性声明
    /// </summary>
    public interface ICaculateProperty : IProperty
    {
        /// <summary>
        /// 表达式
        /// </summary>
        string Expression { get; }
        /// <summary>
        /// 值提供者
        /// </summary>
        Func<Entity, object> ValueProvider { get; }

        IProperty[] Dependencies { get; }
    }

    /// <summary>
    /// 计算属性声明
    /// </summary>
    /// <typeparam name="T">属性值类型</typeparam>
    public interface ICaculateProperty<T> : IProperty<T>, ICaculateProperty
    {

    }

    class CaculateProperty : Property, ICaculateProperty
    {
        public string Expression { get; set; }

        public Func<Entity, object> ValueProvider { get; set; }

        public IProperty[] Dependencies { get; set; }
    }

    class CaculateProperty<T> : CaculateProperty, ICaculateProperty<T>
    {
    }
}
