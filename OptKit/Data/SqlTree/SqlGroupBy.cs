namespace OptKit.Data.SqlTree
{
    /// <summary>
    /// 聚合分组。
    /// </summary>
    class SqlGroupBy : SqlNode, IGroupBy
    {
        public override SqlNodeType NodeType { get { return SqlNodeType.SqlGroupBy; } }

        /// <summary>
        /// 使用这个列进行排序。
        /// </summary>
        public SqlColumn Column { get; set; }
    }
}
