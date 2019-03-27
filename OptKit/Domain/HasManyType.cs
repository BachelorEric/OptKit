using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain
{
    /// <summary>
    /// 一对多子属性的类型
    /// </summary>
    public enum HasManyType
    {
        /// <summary>
        /// 组合（子对象）
        /// </summary>
        Composition,

        /// <summary>
        /// 聚合（简单引用）
        /// </summary>
        Aggregation
    }
}
