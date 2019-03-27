using OptKit.Domain;
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
}
