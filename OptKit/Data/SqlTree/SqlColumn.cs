using System.Collections;
using System.Collections.Generic;

namespace OptKit.Data.SqlTree
{
    /// <summary>
    /// 表示某个表、或者查询结果中的某一列。
    /// </summary>
    class SqlColumn : SqlNode
    {
        public override SqlNodeType NodeType { get { return SqlNodeType.SqlColumn; } }

        /// <summary>
        /// 只能是<see cref="SqlTable"/>、<see cref="SqlSubSelect"/>
        /// </summary>
        public SqlNamedSource Table { get; set; }

        public string ColumnName { get; set; }

        /// <summary>
        /// 别名。
        /// 列的别名只用在 Select 语句之后。
        /// </summary>
        public string Alias { get; set; }
    }
}