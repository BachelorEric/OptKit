namespace OptKit.Data.SqlTree
{
    /// <summary>
    /// 表示一个取反规则的条件。
    /// </summary>
    class SqlNotConstraint : SqlConstraint
    {
        public override SqlNodeType NodeType { get { return SqlNodeType.SqlNotConstraint; } }

        /// <summary>
        /// 需要被取反的条件。
        /// </summary>
        public SqlConstraint Constraint { get; set; }
    }
}
