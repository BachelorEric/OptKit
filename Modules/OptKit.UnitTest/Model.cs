using OptKit.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.UnitTest
{
    public class Model : IEntity
    {
        public object Id { get; set; }

        public string StringProperty { get; set; }
    }
}
