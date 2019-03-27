using System;

namespace OptKit.Data.SqlTree
{
    /// <summary>
    /// 表示一个具体的表或视图。
    /// </summary>
    class SqlTable : SqlNamedSource
    {
        public override SqlNodeType NodeType { get { return SqlNodeType.SqlTable; } }

        public string Schema { get; set; }

        public string TableName { get; set; }

        public string ViewSql { get; set; }

        public Func<ISqlSelect> View { get; set; }

        public string Alias { get; set; }

        public override string GetName()
        {
            return Alias ?? TableName;
        }
    }
}
