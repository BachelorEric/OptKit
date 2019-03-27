using System.Diagnostics;

namespace OptKit.Data.SqlTree
{
    /// <summary>
    /// 表示 Sql 语法树中的一个节点。
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay}")]
    abstract class SqlNode : ISqlNode
    {
        /// <summary>
        /// 返回当前树节点的类型。
        /// </summary>
        /// <value>
        /// The type of the node.
        /// </value>
        public abstract SqlNodeType NodeType { get; }

        string DebuggerDisplay
        {
            get
            {
                //var generator = new SqlServerSqlGenerator();
                //generator.Generate(this);
                //return string.Format(generator.Sql, generator.Sql.Parameters);
                return "";
            }
        }
    }
}