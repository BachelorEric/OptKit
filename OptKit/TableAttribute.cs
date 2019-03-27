using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit
{
    /// <summary>
    /// 表影射标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// SQL视图名或者SQL查询语句
        /// </summary>
        public string SqlView { get; set; }
        /// <summary>
        /// SQL视图查询生成器
        /// </summary>
        public Type ViewProvider { get; set; }

        public TableAttribute() { }

        public TableAttribute(string tableName)
        {
            TableName = tableName;
        }
    }
}
