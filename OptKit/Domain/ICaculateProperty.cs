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
        string Expression { get; set; }
        Func<object> ValueProvider { get; set; }
    }

    /// <summary>
    /// 计算属性声明
    /// </summary>
    /// <typeparam name="T">属性值类型</typeparam>
    public interface ICaculateProperty<T> : ICaculateProperty
    {

    }
}
