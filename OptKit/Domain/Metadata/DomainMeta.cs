using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace OptKit.Domain.Metadata
{
    /// <summary>
    /// 实体元数据
    /// </summary>
    [DebuggerDisplay("DomainMeta:{Name} [{Caption}]")]
    public class DomainMeta
    {
        internal List<DataPropertyMeta> PropertiesInternal { get; } = new List<DataPropertyMeta>();
        internal List<ChildPropertyMeta> ChildPropertiesInternal { get; } = new List<ChildPropertyMeta>();
        internal List<DomainMeta> ChildrenInternal { get; } = new List<DomainMeta>();

        /// <summary>
        /// 标题
        /// </summary>
        public string Caption { get; internal set; }

        /// <summary>
        /// 实体名称
        /// </summary>
        public string Name { get { return DomainType.Name; } }

        /// <summary>
        /// 实体标记
        /// </summary>
        public DomainAttribute Attribute { get; internal set; }

        /// <summary>
        /// 对象类型
        /// </summary>
        public Type DomainType { get; internal set; }

        /// <summary>
        /// 如果这个属性不为 null，表示该实体映射到数据库的表。
        /// </summary>
        public TableMeta TabelMeta { get; internal set; }

        /// <summary>
        /// 属性。
        /// </summary>
        public IReadOnlyCollection<DataPropertyMeta> Properties { get { return PropertiesInternal; } }

        /// <summary>
        /// 关联的子属性。
        /// </summary>
        public IReadOnlyCollection<ChildPropertyMeta> ChildProperties { get { return ChildPropertiesInternal; } }

        /// <summary>
        /// 聚合根
        /// </summary>
        public DomainMeta AggregationRoot { get { return Parent != null ? Parent.AggregationRoot : this; } }

        /// <summary>
        /// parent-child关系中的parent
        /// </summary>
        public DomainMeta Parent { get; internal set; }

        /// <summary>
        /// parent-child关系中的child集合。父子关系需要是双向关联，且子实体中的引用类型为<see cref="ReferenceType.Parent"/>
        /// </summary>
        public IReadOnlyCollection<DomainMeta> Children { get { return ChildrenInternal; } }

        public DataPropertyMeta Property(string propertyName)
        {
            return PropertiesInternal.Find(p => p.Equals(propertyName));
        }
        public ChildPropertyMeta ChildProperty(string propertyName)
        {
            return ChildPropertiesInternal.Find(p => p.Equals(propertyName));
        }
        public DataPropertyMeta Property(IProperty property)
        {
            return PropertiesInternal.Find(p => p.Equals(property));
        }
        public ChildPropertyMeta ChildProperty(IListProperty property)
        {
            return ChildPropertiesInternal.Find(p => p.Equals(property));
        }
    }
}
