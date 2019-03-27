using OptKit.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.UnitTest
{
    [Table("TST_MODEL_B")]
    public class ModelB : ModelA
    {
        public virtual double Qty { get; set; }
    }
}
