using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace OptKit.Domain
{
    [DebuggerDisplay("{GetType().Name}:{Name}")]
    public abstract class Property : IProperty
    {
        public string Name { get; internal set; }
        public Type PropertyType { get; internal set; }
        public Type OwnerType { get; internal set; }
        public Type DeclareType { get; internal set; }
        public Attribute[] Attributes { get; internal set; }
        internal int GlobalIndex { get; set; } = -1;
        public int CompiledIndex { get; internal set; } = -1;

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

    class DataProperty : Property, IDataProperty { }

    class DataProperty<T> : DataProperty, IDataProperty<T> { }

    class CaculateProperty : Property, ICaculateProperty
    {
        public string Expression { get; set; }

        public Func<DomainObject, object> ValueProvider { get; set; }

        public IProperty[] Dependencies { get; set; }
    }

    class CaculateProperty<T> : CaculateProperty, ICaculateProperty<T> { }

    class RefProperty : Property, IRefProperty
    {
        public IRefIdProperty RefIdProperty { get; set; }

        public IRefEntityProperty RefEntityProperty { get; set; }
    }

    class RefIdProperty : RefProperty, IRefIdProperty
    {
        public ReferenceType ReferenceType { get; set; }
    }

    class RefIdProperty<T> : RefIdProperty, IRefIdProperty<T> { }

    class RefEntityProperty : RefProperty, IRefEntityProperty { }

    class RefEntityProperty<T> : RefProperty, IRefEntityProperty<T> { }

    class ListProperty : Property, IListProperty
    {
        public Type ItemType { get; set; }

        public HasManyType HasManyType { get; set; }
    }

    class ListProperty<T> : ListProperty, IListProperty<T> { }

    class ViewProperty : Property, IViewProperty
    {
        public string ViewPath { get; set; }
    }

    class ViewProperty<T> : ViewProperty, IViewProperty<T> { }
}
