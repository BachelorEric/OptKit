using OptKit.Logging;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace OptKit.Log4Net
{
    /// <summary>
    /// Log4Net日志工厂适配器
    /// </summary>
    public class Log4NetLoggerFactoryAdapter : ILoggerFactoryAdapter
    {
        public ILog GetLogger(string key)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(ILoggerFactoryAdapter).Assembly, key);
            return new Log4NetLogger(log);
        }

        public ILog GetLogger(Type type)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(type);
            return new Log4NetLogger(log);
        }
    }
}
