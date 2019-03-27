namespace OptKit.Data.SqlTree
{
    /// <summary>
    /// 一个数据源与一个具体表的连接结果，同时它也是一个新的数据源。
    /// </summary>
    class SqlJoin : SqlSource
    {
        public override SqlNodeType NodeType { get { return SqlNodeType.SqlJoin; } }

        public SqlSource Left { get; set; }

        public SqlJoinType JoinType { get; set; }

        public SqlTable Right { get; set; }

        /// <summary>
        /// 连接所使用的约束条件。
        /// </summary>
        public SqlConstraint Condition { get; set; }
    }

    /// <summary>
    /// 支持的连接方式。
    /// </summary>
    enum SqlJoinType
    {
        Inner,
        LeftOuter,
        RightOuter
        //Cross,
        //CrossApply,
        //OuterApply
    }
}
