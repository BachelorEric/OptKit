﻿using System.Collections;
using System.Collections.Generic;

namespace OptKit.Data.SqlTree
{
    /// <summary>
    /// 聚合分组。
    /// </summary>
    class SqlGroupByList : SqlNode, IEnumerable
    {
        IList _items;

        public override SqlNodeType NodeType { get { return SqlNodeType.SqlGroupByList; } }

        public int Count
        {
            get { return _items?.Count ?? 0; }
        }

        public IList Items
        {
            get { return _items ?? (_items = new List<SqlGroupBy>()); }
            set { _items = value; }
        }

        public void Add(object item)
        {
            Items.Add(item);
        }

        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
