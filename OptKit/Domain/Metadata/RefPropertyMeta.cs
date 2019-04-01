using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain.Metadata
{
    public class RefPropertyMeta : DataPropertyMeta
    {
        /// <summary>
        /// 引用属性
        /// </summary>
        public IRefProperty RefProperty { get { return Property as IRefProperty; } }

        protected internal override bool Equals(string name)
        {
            return base.Equals(name) || RefProperty.RefEntityProperty.Name == name;
        }
        protected internal override bool Equals(IProperty property)
        {
            return base.Equals(property) || RefProperty.RefEntityProperty == property;
        }
    }
}
