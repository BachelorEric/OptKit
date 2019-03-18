using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain
{
    /// <summary>
    /// 实体声明，实体是具有唯一主键<see cref="Id"/>的对象
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        object Id { get; set; }
    }
}
