using System.Collections;
using System.Collections.Generic;

namespace OptKit.Data.SqlTree
{
    /// <summary>
    /// 表示一组结点对象
    /// 
    /// SqlNodeList 需要从 SqlConstraint 上继承，否则将不可用于 Where 语句。
    /// </summary>
    class SqlNodeList : SqlConstraint, IEnumerable, ISqlLiteral
    {
        public override SqlNodeType NodeType { get { return SqlNodeType.SqlNodeList; } }

        public void Add(ISqlNode item)
        {
            Items.Add(item);
        }

        public List<ISqlNode> Items { get; } = new List<ISqlNode>();

        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}