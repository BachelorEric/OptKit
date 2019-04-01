using OptKit.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain.Mapping
{
    public class RdbColumn
    {
        ISqlDialect _dialect;

        internal RdbColumn(ISqlDialect dialect)
        {
            _dialect = dialect;
        }

        public RdbTable Table { get; }

        public IProperty Property { get; internal set; }

        public string Name { get; internal set; }

        public string View { get; internal set; }

        public string Formula { get; internal set; }
        /// <summary>
        /// 是否可空列。
        /// </summary>
        public bool IsNullable { get; internal set; }

        /// <summary>
        /// 是否主键列
        /// </summary>
        public bool IsPrimaryKey { get; internal set; }

        /// <summary>
        /// 是否可插入
        /// </summary>
        public bool IsInsertable { get; set; }

        /// <summary>
        /// 是否可更新
        /// </summary>
        public bool IsUpdateable { get; set; }

        /// <summary>
        /// 是否版本
        /// </summary>
        public bool IsVersioned { get; internal set; }

        public virtual object GetValue(Entity entity)
        {
            var value = entity.Get(Property);
            return _dialect.PrepareValue(value);
        }

        public virtual void LoadValue(Entity entity, object value)
        {
            entity.Load(value, Property);
        }
    }
}
