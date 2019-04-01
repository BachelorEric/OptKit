using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Context
{
    /// 服务器端上下文提供器。
    /// 默认实现：一个标记了 ThreadStatic 的字段。
    /// </summary>
    public class ThreadStaticAppContextProvider : IAppContextProvider
    {
        [ThreadStatic]
        private static IDictionary<string, object> _items;

        /// <summary>
        /// 当前线程所使用的项的集合。
        /// </summary>
        public static IDictionary<string, object> Items
        {
            get { return _items ?? (_items = new Dictionary<string, object>()); }
        }

        public IDictionary<string, object> Context
        {
            get { return _items; }
            set { _items = value; }
        }
    }
}