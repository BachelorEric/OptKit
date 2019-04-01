using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain
{
    /// <summary>
    /// 实体列表声明
    /// </summary>
    public interface IDomainList
    {
        IList Deleted { get; }
    }
}
