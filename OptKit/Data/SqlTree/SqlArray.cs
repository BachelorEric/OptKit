using System.Collections;
using System.Collections.Generic;

namespace OptKit.Data.SqlTree
{
    /// <summary>
    /// 表示一组树结点合成的一个集合结点。
    /// 这些结点之间，需要用逗号分隔开。
    /// </summary>
    class SqlArray : SqlNode
    {
        public SqlArray(bool initItems = true)
        {
            if (initItems)
            {
                Items = new List<SqlNode>();
            }
        }

        public override SqlNodeType NodeType { get { return SqlNodeType.SqlArray; } }

        /// <summary>
        /// 所有项。
        /// 其中每一个项必须是一个 SqlNode。
        /// </summary>
        public IList Items { get; set; }
    }
}