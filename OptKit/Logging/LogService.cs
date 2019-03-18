using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptKit.Logging
{
    /// <summary>
    /// Log Service to get ILog
    /// </summary>
    public static class LogService
    {
        /// <summary>
        /// Default Logger named app_logger
        /// </summary>
        public static ILog Logger { get; private set; } = GetLogger("app_logger");

        static ILoggerFactoryAdapter factory;

        static ILoggerFactoryAdapter Factory
        {
            get
            {
                if (factory == null)
                {
                    var adapter = RT.Config.Get<string>("loggerFactoryAdapter");
                    if (adapter.IsNotEmpty())
                    {
                        var type = Type.GetType(adapter);
                        if (type != null)
                            factory = (ILoggerFactoryAdapter)Activator.CreateInstance(type);
                    }
                }
                return factory ?? (factory = new TraceLoggerFactory());
            }
        }

        /// <summary>
        /// Set ILoggerFactoryAdapter
        /// </summary>
        /// <param name="loggerFactoryAdapter"></param>
        public static void SetFactory(ILoggerFactoryAdapter loggerFactoryAdapter)
        {
            factory = loggerFactoryAdapter;
            Logger = GetLogger("app_logger");
        }

        /// <summary>
        /// Gets the logger by calling <see cref="ILoggerFactoryAdapter.GetLogger(Type)"/>
        /// on the currently configured using the specified type.
        /// </summary>
        /// <returns>the logger instance obtained from the current</returns>
        public static ILog GetLogger<T>()
        {
            return Factory.GetLogger(typeof(T));
        }

        /// <summary>
        /// Gets the logger by calling <see cref="ILoggerFactoryAdapter.GetLogger(Type)"/>
        /// on the currently configured using the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>the logger instance obtained from the current</returns>
        public static ILog GetLogger(Type type)
        {
            return Factory.GetLogger(type);
        }

        /// <summary>
        /// Gets the logger by calling <see cref="ILoggerFactoryAdapter.GetLogger(string)"/>
        /// on the currently configured using the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>the logger instance obtained from the current</returns>
        public static ILog GetLogger(string key)
        {
            return Factory.GetLogger(key);
        }
    }
}
