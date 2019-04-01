using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit
{
    /// <summary>
    /// 表映射标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        public TableAttribute() { }

        public TableAttribute(string tableName)
        {
            TableName = tableName;
        }

        public TableAttribute(Type sqlViewType)
        {
            SqlViewType = sqlViewType;
        }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// SQL视图名或者SQL查询语句
        /// </summary>
        public string SqlView { get; set; }
        /// <summary>
        /// SQL视图查询生成器, 需要实现<see cref="ISqlView"/>
        /// </summary>
        public Type SqlViewType { get; set; }
    }
}
