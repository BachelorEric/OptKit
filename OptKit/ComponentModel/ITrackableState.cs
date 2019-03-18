using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.ComponentModel
{
    public interface ITrackableState: ITrackable
    {
        /// <summary>
        /// 状态变更事件
        /// </summary>
        event EventHandler StateChanged;

        /// <summary>
        /// 状态
        /// </summary>
        TrackableState State { get; }

        /// <summary>
        /// 状态标记为新对象.
        /// </summary>
        void MarkNew();

        /// <summary>
        /// 状态标记为修改.
        /// </summary>
        void MarkModified();

        /// <summary>
        /// 状态标记为删除.
        /// </summary>
        void MarkDeleted();

        /// <summary>
        /// 状态标记为没变更.
        /// </summary>
        void ResetState();
    }
}
