using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptKit.Logging
{
    /// <summary>
    /// Trace日志适配器
    /// </summary>
    public class TraceLoggerFactory : ILoggerFactoryAdapter
    {
        TraceLogger logger = new TraceLogger();
        /// <summary>
        /// 根据键获取<see cref="ILog"/>实例.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ILog GetLogger(string key)
        {
            return logger;
        }

        /// <summary>
        /// 根据类型获取<see cref="ILog"/>实例.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ILog GetLogger(Type type)
        {
            return logger;
        }
    }
}
