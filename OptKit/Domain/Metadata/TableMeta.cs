using OptKit.Data.SqlTree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace OptKit.Domain.Metadata
{
    /// <summary>
    /// 表映射元数据
    /// </summary>
    [DebuggerDisplay("TableMeta:{TableName}{SqlView}")]
    public class TableMeta
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 视图名或者SQL语句
        /// </summary>
        public string SqlView { get; set; }

        /// <summary>
        /// 动态查询
        /// </summary>
        public Func<ISqlSelect> QueryView { get; set; }
    }
}
