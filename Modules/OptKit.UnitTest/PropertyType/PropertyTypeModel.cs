using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.UnitTest.PropertyType
{
    public class PropertyTypeModel : Entity<double>
    {
        public virtual string StringProperty { get; set; }

        public virtual double DoubleProperty { get; set; }

        public virtual double? NullableProperty { get; set; }
    }
}
