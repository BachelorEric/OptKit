using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain.Metadata
{
    public class DataPropertyMeta : PropertyMeta
    {
        /// <summary>
        /// 如果这个属性不为 null，表示该属性映射到数据库的字段。
        /// </summary>
        public ColumnMeta ColumnMeta { get; internal set; }
    }
}
