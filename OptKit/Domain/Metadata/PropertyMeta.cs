using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace OptKit.Domain.Metadata
{
    /// <summary>
    /// 属性元数据
    /// </summary>
    [DebuggerDisplay("{GetType().Name}:{Property.Name} [{Caption}]")]
    public class PropertyMeta
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Caption { get; internal set; }
        /// <summary>
        /// 属性
        /// </summary>
        public IProperty Property { get; internal set; }

        /// <summary>
        /// 所有者
        /// </summary>
        public DomainMeta Owner { get; internal set; }

        /// <summary>
        /// 特性
        /// </summary>
        public Attribute[] Attributes { get; internal set; }

        protected internal virtual bool Equals(string name)
        {
            return Property.Name == name;
        }
        protected internal virtual bool Equals(IProperty property)
        {
            return Property == property;
        }
    }
}
