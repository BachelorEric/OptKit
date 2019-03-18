using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptKit.ComponentModel
{
    /// <summary>
    /// 实例被创建事件的参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EventArgs<T> : EventArgs
        where T : class
    {
        public EventArgs(T instance)
        {
            this.Instance = instance;
        }

        /// <summary>
        /// 被创建的实例
        /// </summary>
        public T Instance { get; private set; }
    }
}

