using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OptKit
{
    /// <summary>
    /// 字段映射标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ColumnAttribute : Attribute
    {
        /// <summary>
        /// 是否主键
        /// </summary>
        internal bool IsPrimaryKey { get; set; }

        public ColumnAttribute() { }

        internal ColumnAttribute(bool key)
        {
            IsPrimaryKey = key;
        }

        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 表示引用属性在数据库中是否有外键约束
        /// </summary>
        public bool? HasForeignKey { get; internal set; }

        /// <summary>
        /// 是否索引列
        /// </summary>
        public bool IsIndexer { get; internal set; }

        /// <summary>
        /// 数据库是否允许为空
        /// </summary>
        public bool? IsNullable { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public DbType? DbType { get; set; }

        /// <summary>
        /// 字段长度
        /// </summary>
        public string DbTypeLength { get; set; }

        /// <summary>
        /// 映射数据库中的字段的默认值。
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// 是否可插入
        /// </summary>
        public bool IsInsertable { get; set; } = true;

        /// <summary>
        /// 是否可更新
        /// </summary>
        public bool IsUpdateable { get; set; } = true;

        /// <summary>
        /// 是否使用序列
        /// </summary>
        public bool UseSequence { get; set; }
    }
}
