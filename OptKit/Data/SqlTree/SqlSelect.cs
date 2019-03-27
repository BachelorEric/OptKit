namespace OptKit.Data.SqlTree
{
    /// <summary>
    /// 表示一个 Sql 查询语句。
    /// </summary>
    class SqlSelect : SqlNode, ISqlSelect
    {
        public override SqlNodeType NodeType { get { return SqlNodeType.SqlSelect; } }

        /// <summary>
        /// 是否只查询数据的条数。
        /// 
        /// 如果这个属性为真，那么不再需要使用 Selection。
        /// </summary>
        public bool IsCounting { get; set; }

        /// <summary>
        /// 是否需要查询不同的结果。
        /// </summary>
        public bool IsDistinct { get; set; }

        /// <summary>
        /// 要查询的内容。
        /// 如果本属性为空，表示要查询所有列。
        /// </summary>
        public ISqlNode Selection { get; set; }

        /// <summary>
        /// 要查询的数据源。
        /// </summary>
        public ISqlSource From { get; set; }

        /// <summary>
        /// 查询的过滤条件。
        /// </summary>
        public ISqlConstraint Where { get; set; }

        /// <summary>
        /// 查询的过滤条件。
        /// </summary>
        public ISqlConstraint Having { get; set; }

        /// <summary>
        /// 查询的排序规则。
        /// 可以指定多个排序条件，其中每一项都必须是一个 SqlOrderBy 对象。
        /// </summary>
        public SqlOrderByList OrderBy { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        public SqlGroupByList GroupBy { get; set; }

        internal bool HasOrdered()
        {
            return OrderBy?.Count > 0;
        }

        internal bool HasGroup()
        {
            return GroupBy?.Count > 0;
        }
    }
}