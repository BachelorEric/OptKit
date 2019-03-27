namespace OptKit.Data.SqlTree
{
    /// <summary>
    /// 表示一个文本
    /// </summary>
    class SqlLiteral : SqlConstraint, ISqlLiteral
    {
        public SqlLiteral() { }

        public SqlLiteral(string formattedSql)
        {
            FormattedSql = formattedSql;
        }

        public override SqlNodeType NodeType { get { return SqlNodeType.SqlLiteral; } }

        public string FormattedSql { get; set; }

        public object[] Parameters { get; set; }
    }
}
