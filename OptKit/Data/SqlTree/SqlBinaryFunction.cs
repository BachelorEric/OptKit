using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptKit.Data.SqlTree
{
    /// <summary>
    /// 比较函数
    /// </summary>
    class SqlBinaryFunction : SqlFunction
    {
        public override SqlNodeType NodeType { get { return SqlNodeType.SqlBinaryFunctioin; } }

        public SqlNode Left { get { return Args.Items?.OfType<SqlNode>().FirstOrDefault(); } }

        public SqlNode Right { get { return Args.Items?.OfType<SqlNode>().LastOrDefault(); } }
    }
}
