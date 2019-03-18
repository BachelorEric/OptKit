using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptKit.ComponentModel
{
    /// <summary>
    /// 可追踪状态
    /// </summary>
    public interface ITrackable : INotifyValueChanged
    {
        /// <summary>
        /// 禁用变更通知
        /// </summary>
        void SuppressNotifyChanged();
        /// <summary>
        /// 恢复变更通知
        /// </summary>
        void ResumeNotifyChanged();
        /// <summary>
        /// 触发属性变更通知
        /// </summary>
        /// <param name="propertyName">变更的属性名称</param>
        void RaisePropertyChanged(string propertyName);
        /// <summary>
        /// 触发属性值变更通知
        /// </summary>
        /// <param name="propertyNamel"></param>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        void RaiseValueChanged(string propertyName, object newValue, object oldValue);
    }
}

