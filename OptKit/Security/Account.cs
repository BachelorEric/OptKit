using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Security
{
    [Serializable]
    [Table(SqlView = "SYS_USER")]
    public class Account : NamedEntity
    {
        public virtual string Code { get; set; }
    }
}
