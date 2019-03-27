using System.ComponentModel;

namespace OptKit.Data.SqlTree
{
    /// <summary>
    /// 排序结点。
    /// </summary>
    class SqlOrderBy : SqlNode, IOrderBy
    {
        public override SqlNodeType NodeType { get { return SqlNodeType.SqlOrderBy; } }

        /// <summary>
        /// 使用这个列进行排序。
        /// </summary>
        public SqlColumn Column { get; set; }

        /// <summary>
        /// 使用这个方向进行排序。
        /// </summary>
        public ListSortDirection Direction { get; set; }
    }
}
