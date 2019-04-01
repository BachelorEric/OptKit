using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.UnitTest.ClrProperty
{
    public class RefEntity : Entity<double>
    {
        public virtual double RefId { get; set; }
        public virtual Ref Ref { get; set; }
        public virtual double? SelfId { get; set; }
        public virtual RefEntity Self { get; set; }
    }
    public class Ref : Entity<double>
    {

    }
}
