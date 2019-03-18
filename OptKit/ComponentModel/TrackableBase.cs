using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OptKit.ComponentModel
{
    /// <summary>
    /// 可追踪的对象，实现<see cref="INotifyPropertyChanged"/>和<see cref="ITrackable"/>。当对象属性发生变化时，会触发<see cref="PropertyChanged"/>和<see cref="ValueChanged"/>变更事件, 可以通过变更事件跟踪对象的变化。
    /// </summary>
    [Serializable]
    public class TrackableBase : INotifyPropertyChanged, ITrackable
    {
        [NonSerialized]
        bool _suppressNotifyChanged;
        [NonSerialized]
        PropertyChangedEventHandler _propertyChanged;
        [NonSerialized]
        EventHandler<ValueChangedEventArgs> _valueChanged;

        /// <summary>
        /// 属性变更事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { _propertyChanged = (PropertyChangedEventHandler)Delegate.Combine(_propertyChanged, value); }
            remove { _propertyChanged = (PropertyChangedEventHandler)Delegate.Remove(_propertyChanged, value); }
        }

        /// <summary>
        /// 属性值变更事件
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> ValueChanged
        {
            add { _valueChanged = (EventHandler<ValueChangedEventArgs>)Delegate.Combine(_valueChanged, value); }
            remove { _valueChanged = (EventHandler<ValueChangedEventArgs>)Delegate.Remove(_valueChanged, value); }
        }

        /// <summary>
        /// 属性变更后
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (!_suppressNotifyChanged)
                _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 属性值变更后
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="newValue">新值</param>
        /// <param name="oldValue">旧值</param>
        protected virtual void OnValueChanged(string propertyName, object newValue, object oldValue)
        {
            if (!_suppressNotifyChanged)
                _valueChanged?.Invoke(this, new ValueChangedEventArgs(propertyName, newValue, oldValue));
        }

        /// <summary>
        /// 触发属性变更通知
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        public void RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// 触发属性值变更通知
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="newValue">新值</param>
        /// <param name="oldValue">旧值</param>
        public void RaiseValueChanged(string propertyName, object newValue, object oldValue)
        {
            OnValueChanged(propertyName, newValue, oldValue);
        }

        /// <summary>
        /// 恢复属性变更通知
        /// </summary>
        public void ResumeNotifyChanged()
        {
            _suppressNotifyChanged = false;
        }

        /// <summary>
        /// 停止属性变更通知，修改值是不会触发变更事件，直到调<see cref="ResumeNotifyChanged"/>
        /// </summary>
        public void SuppressNotifyChanged()
        {
            _suppressNotifyChanged = true;
        }


        /// <summary>
        /// 创建<see cref="TrackableBase"/>实例的代理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Create<T>() where T : TrackableBase
        {
            return TrackableFactory.Create<T>();
        }
    }
}
