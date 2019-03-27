namespace OptKit.Data.SqlTree
{
    /// <summary>
    /// 表示对指定的查询进行是否存在查询行的逻辑的判断。
    /// </summary>
    class SqlExistsConstraint : SqlConstraint
    {
        public override SqlNodeType NodeType { get { return SqlNodeType.SqlExistsConstraint; } }

        public SqlSelect Select { get; set; }

        public bool IsNot { get; set; }
    }
}
