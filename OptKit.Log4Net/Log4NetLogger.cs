using OptKit.Logging;
using System;

namespace OptKit.Log4Net
{
    class Log4NetLogger : ILog
    {
        private readonly log4net.ILog _log;

        public Log4NetLogger(log4net.ILog log)
        {
            _log = log;
        }

        public bool IsDebugEnabled
        {
            get { return _log.IsDebugEnabled; }
        }

        public bool IsErrorEnabled
        {
            get { return _log.IsErrorEnabled; }
        }

        public bool IsFatalEnabled
        {
            get { return _log.IsFatalEnabled; }
        }

        public bool IsInfoEnabled
        {
            get { return _log.IsInfoEnabled; }
        }

        public bool IsWarnEnabled
        {
            get { return _log.IsWarnEnabled; }
        }

        public void Debug(object message)
        {
            _log.Debug(message);
        }

        public void Debug(object message, Exception exception)
        {
            _log.Debug(message, exception);
        }

        public void DebugFormat(string format, params object[] args)
        {
            _log.DebugFormat(format, args);
        }

        public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            _log.DebugFormat(formatProvider, format, args);
        }

        public void DebugFormat(string format, Exception exception, params object[] args)
        {
            _log.DebugFormat(format, exception, args);
        }

        public void DebugFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            _log.DebugFormat(formatProvider, format, exception, args);
        }

        public void Error(object message)
        {
            _log.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            _log.Error(message, exception);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            _log.ErrorFormat(format, args);
        }

        public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            _log.ErrorFormat(formatProvider, format, args);
        }

        public void ErrorFormat(string format, Exception exception, params object[] args)
        {
            _log.ErrorFormat(format, exception, args);
        }

        public void ErrorFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            _log.ErrorFormat(formatProvider, format, exception, args);
        }

        public void Fatal(object message)
        {
            _log.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            _log.Fatal(message, exception);
        }

        public void FatalFormat(string format, params object[] args)
        {
            _log.FatalFormat(format, args);
        }

        public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            _log.FatalFormat(formatProvider, format, args);
        }

        public void FatalFormat(string format, Exception exception, params object[] args)
        {
            _log.FatalFormat(format, exception, args);
        }

        public void FatalFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            _log.FatalFormat(formatProvider, format, exception, args);
        }

        public void Info(object message)
        {
            _log.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            _log.Info(message, exception);
        }

        public void InfoFormat(string format, params object[] args)
        {
            _log.InfoFormat(format, args);
        }

        public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            _log.InfoFormat(formatProvider, format, args);
        }

        public void InfoFormat(string format, Exception exception, params object[] args)
        {
            _log.InfoFormat(format, exception, args);
        }

        public void InfoFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            _log.InfoFormat(formatProvider, format, exception, args);
        }

        public void Warn(object message)
        {
            _log.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            _log.Warn(message, exception);
        }

        public void WarnFormat(string format, params object[] args)
        {
            _log.WarnFormat(format, args);
        }

        public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            _log.WarnFormat(formatProvider, format, args);
        }

        public void WarnFormat(string format, Exception exception, params object[] args)
        {
            _log.WarnFormat(format, exception, args);
        }

        public void WarnFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            _log.WarnFormat(formatProvider, format, exception, args);
        }
    }
}
