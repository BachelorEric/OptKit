using OptKit.Domain;
using OptKit.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit
{
    /// <summary>
    /// 实体，主键的类型为<see cref="Key"/>
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    [Serializable]
    public class Entity<Key> : Entity
    {
        /// <summary>
        /// 类型为<see cref="Key"/>的主键
        /// </summary>
        [Column(true)]
        public virtual Key Id { get; set; }

        /// <summary>
        /// 获取主键的值
        /// </summary>
        /// <returns></returns>
        protected override object GetId()
        {
            return Id;
        }
        /// <summary>
        /// 设置主键的值
        /// </summary>
        /// <param name="id"></param>
        protected override void SetId(object id)
        {
            Id = id.ConvertTo<Key>();
        }
    }
    /// <summary>
    /// 查询条件
    /// </summary>
    [Serializable]
    public class Criteria : ValueObject
    {
    }
    /// <summary>
    /// 视图模块
    /// </summary>
    [Serializable]
    public class ViewModel : ValueObject
    {
    }
    /// <summary>
    /// 业务实体
    /// </summary>
    [Serializable]
    public class DataEntity : Entity<double>
    {
        public virtual DateTime CreateTime { get; set; }

        public virtual double CreatorId { get; set; }

        public virtual Account Creator { get; set; }

        public virtual DateTime UpdateTime { get; set; }

        public virtual double UpdatorId { get; set; }

        public virtual Account Updator { get; set; }
    }
    /// <summary>
    /// 命名实体
    /// </summary>
    [Serializable]
    public class NamedEntity : DataEntity, INamedEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }
    }
}
