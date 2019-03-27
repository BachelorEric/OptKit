namespace OptKit.Data.SqlTree
{
    /// <summary>
    /// 子查询。
    /// 对一个子查询分配别名后，可以作为一个新的源。
    /// </summary>
    class SqlSubSelect : SqlNamedSource
    {
        public override SqlNodeType NodeType { get { return SqlNodeType.SqlSubSelect; } }

        /// <summary>
        /// 子查询
        /// </summary>
        public SqlSelect Select { get; set; }

        /// <summary>
        /// 别名，必须填写
        /// </summary>
        public string Alias { get; set; }

        public override string GetName()
        {
            return Alias;
        }
    }
}
