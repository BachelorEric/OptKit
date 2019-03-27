using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptKit.Data.SqlTree
{
    class SqlBinaryConstraint : SqlConstraint
    {
        public override SqlNodeType NodeType { get { return SqlNodeType.SqlBinaryConstraint; } }

        /// <summary>
        /// 第一个需要对比的列。
        /// </summary>
        public SqlNode Left { get; set; }

        /// <summary>
        /// 第二个需要对比的列。
        /// </summary>
        public SqlNode Right { get; set; }

        /// <summary>
        /// 对比条件。
        /// </summary>
        public SqlBinaryOperator Operator { get; set; } = SqlBinaryOperator.Equal;
    }
}
