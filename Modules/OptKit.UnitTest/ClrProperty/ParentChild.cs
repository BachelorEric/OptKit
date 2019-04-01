using OptKit.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.UnitTest.ClrProperty
{
    [Table]
    public class Parent : Entity<int>
    {
        public virtual string Name { get; set; }

        public virtual DomainList<Child> Children { get; }
    }

    [Caption("子")]
    [Table]
    [Domain(Category = DomainCategory.Child)]
    public class Child : Entity<int>
    {
        public virtual string Name { get; set; }

        public virtual Parent Parent { get; set; }

        public virtual int ParentId { get; set; }
    }
}
