using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain.Metadata
{
    public class ChildPropertyMeta : PropertyMeta
    {
        /// <summary>
        /// 子元数据
        /// </summary>
        public DomainMeta ChildMeta { get; internal set; }
    }
}
