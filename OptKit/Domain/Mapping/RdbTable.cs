using OptKit.Data.SqlTree;
using OptKit.Domain.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain.Mapping
{
    /// <summary>
    /// 数据表
    /// </summary>
    public abstract class RdbTable
    {
        string _insertSql;
        string _deleteSql;

        internal List<RdbColumn> ColumnsInternal { get; } = new List<RdbColumn>();

        public DomainMeta Meta { get; internal set; }

        public string TabelName { get; internal set; }

        public string ViewSql { get; internal set; }

        public Func<ISqlSelect> View { get; internal set; }

        public RdbColumn PKColumn { get; internal set; }

        public RdbColumn VersionColumn { get; internal set; }

        /// <summary>
        /// 本表中可用的所有字段信息。
        /// </summary>
        public IReadOnlyList<RdbColumn> Columns { get { return ColumnsInternal; } }
    }
}
