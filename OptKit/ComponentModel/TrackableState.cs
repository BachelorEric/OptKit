using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptKit.ComponentModel
{
    /// <summary>
    /// 可追踪的状态
    /// </summary>
    public enum TrackableState
    {
        /// <summary>
        /// 未改动
        /// </summary>
        Unchanged = 0,
        /// <summary>
        /// 数据变更
        /// </summary>
        Modified = 1,
        /// <summary>
        /// 新对象
        /// </summary>
        New = 2,
        /// <summary>
        /// 已删除
        /// </summary>
        Deleted = 3
    }
}
