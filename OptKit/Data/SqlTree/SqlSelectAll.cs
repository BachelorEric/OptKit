namespace OptKit.Data.SqlTree
{
    /// <summary>
    /// 用于表示选择所有列、或者表示选择某个表的所有列。
    /// </summary>
    class SqlSelectAll : SqlNode
    {
        public static readonly SqlSelectAll Default = new SqlSelectAll();

        public override SqlNodeType NodeType { get { return SqlNodeType.SqlSelectAll; } }

        /// <summary>
        /// 如果本属性为空，表示选择所有数据源的所有列；否则表示选择指定表的所有列。
        /// </summary>
        public SqlNamedSource Table { get; set; }
    }
}
