using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptKit.Data.SqlTree
{
    /// <summary>
    /// SQL参数值
    /// </summary>
    class SqlValue : SqlNode, ISqlValue
    {
        public override SqlNodeType NodeType { get { return SqlNodeType.SqlValue; } }

        public object Value { get; set; }
    }
}
