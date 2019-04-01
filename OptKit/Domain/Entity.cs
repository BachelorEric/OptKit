using OptKit.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using OptKit.Domain.Data;

namespace OptKit.Domain
{
    /// <summary>
    /// 实体
    /// </summary>
    [Serializable]
    public abstract class Entity : DomainObject, IEntity
    {
        #region ID

        object IEntity.Id
        {
            get => GetId();
            set => SetId(value);
        }

        /// <summary>
        /// 重写此方法获取实体主键<see cref="IEntity.Id"/>的值
        /// </summary>
        /// <returns></returns>
        protected abstract object GetId();

        /// <summary>
        /// 重写此方法设置实体主键<see cref="IEntity.Id"/>的值
        /// </summary>
        /// <param name="id"></param>
        protected abstract void SetId(object id);

        #endregion
    }
}
