using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace OptKit.Domain.Metadata
{
    /// <summary>
    /// 字段映射元数据
    /// </summary>
    [DebuggerDisplay("ColumnMeta:{ColumnName}")]
    public class ColumnMeta
    {
        /// <summary>
        /// 表示引用属性在数据库中是否有外键约束
        /// </summary>
        public bool? HasForeignKey { get; set; }

        /// <summary>
        /// 是否自增长列
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// 是否索引列
        /// </summary>
        public bool IsIndexer { get; set; }

        /// <summary>
        /// 是否使用序列，SqlServer2012+或Oracle支持
        /// </summary>
        public bool UseSequence { get; set; }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 是否可空，如果没有赋值，则按照默认的类型计算方法来计算该值。
        /// </summary>
        public bool? IsNullable { get; set; }

        /// <summary>
        /// 是否版本控制列
        /// </summary>
        public bool IsVersioned { get; set; }

        /// <summary>
        /// 映射数据库中的字段名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 视图路径
        /// </summary>
        public string ViewPath { get; set; }

        /// <summary>
        /// 映射数据库中的字段的类型。
        /// 如果没有设置，则使用默认的映射规则。
        /// </summary>
        public DbType? DbType { get; set; }

        /// <summary>
        /// 映射数据库中的字段的长度、精度等信息。
        /// 可以是数字，也可以是 MAX 等字符串。
        /// 如果是空，则表示使用默认的长度。
        /// </summary>
        public string DbTypeLength { get; set; }

        /// <summary>
        /// 字段的默认值
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// 是否可插入
        /// </summary>
        public bool IsInsertable { get; set; }

        /// <summary>
        /// 是否可更新
        /// </summary>
        public bool IsUpdateable { get; set; }
    }
}
