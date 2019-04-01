using OptKit.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.UnitTest
{
    [Caption("Model B")]
    [Table("TST_MODEL_B")]
    [Domain(DisplayMember = nameof(Name)
        , QueryMembers = new[] { nameof(Description) }
        , UseCriteriaQuery = true
        )]
    public class ModelB : ModelA
    {
        [Caption("描述")]
        [Column()]
        public virtual string Description { get; set; }

        [Caption("数量")]
        [Column()]
        public virtual double Qty { get; set; }
    }
}
