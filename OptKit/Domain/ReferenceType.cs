using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain
{
    /// <summary>
    /// 引用的类型。
    /// </summary>
    public enum ReferenceType
    {
        /// <summary>
        /// 一般的外键引用
        /// </summary>
        Normal,

        /// <summary>
        /// 此引用表示父实体的引用
        /// </summary>
        Parent,
    }
}
