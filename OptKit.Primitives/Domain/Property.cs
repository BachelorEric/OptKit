using OptKit.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Primitives.Domain
{
    class Property : IProperty
    {
        public string PropertyName { get; set; }
        public Type PropertyType { get; set; }
        public Type OwnerType { get; set; }
        public Type DeclareType { get; set; }
    }
}
