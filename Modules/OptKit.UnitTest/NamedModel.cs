using OptKit.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.UnitTest
{
    public class NamedModel : INamedEntity
    {
        public object Id { get; set; }

        public string Name { get; set; }
    }
}
