using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain.Metadata
{
    /// <summary>
    /// 引用信息
    /// </summary>
    public class ReferenceInfo
    {
        /// <summary>
        /// 引用属性
        /// </summary>
        public IRefProperty RefProperty { get; internal set; }
        /// <summary>
        /// 引用实体的元数据
        /// </summary>
        public DomainMeta RefMeta { get; internal set; }
        /// <summary>
        /// 引用类型
        /// </summary>
        public ReferenceType Type { get; internal set; }
    }
}
