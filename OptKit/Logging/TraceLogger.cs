using System;

namespace OptKit.Logging
{
    /// <summary>
    /// Trace日志输出
    /// </summary>
    public class TraceLogger : ILog
    {
        public bool IsDebugEnabled { get { return true; } }

        public bool IsErrorEnabled { get { return true; } }

        public bool IsFatalEnabled { get { return true; } }

        public bool IsInfoEnabled { get { return true; } }

        public bool IsTraceEnabled { get { return true; } }

        public bool IsWarnEnabled { get { return true; } }

        public void Debug(object message)
        {
            Write("Debug\r\n" + message);
        }

        public void Debug(object message, Exception exception)
        {
            Write("Debug\r\n" + message + "\r\n" + exception);
        }

        public void Error(object message)
        {
            Write("Error\r\n" + message);
        }

        public void Error(object message, Exception exception)
        {
            Write("Error\r\n" + message + "\r\n" + exception);
        }

        public void Fatal(object message)
        {
            Write("Fatal\r\n" + message);
        }

        public void Fatal(object message, Exception exception)
        {
            Write("Fatal\r\n" + message + "\r\n" + exception);
        }

        public void Info(object message)
        {
            Write("Info\r\n" + message);
        }

        public void Info(object message, Exception exception)
        {
            Write("Info\r\n" + message + "\r\n" + exception);
        }

        public void Trace(object message)
        {
            Write("Trace\r\n" + message);
        }

        public void Trace(object message, Exception exception)
        {
            Write("Trace\r\n" + message + "\r\n" + exception);
        }

        public void Warn(object message)
        {
            Write("Warn\r\n" + message);
        }

        public void Warn(object message, Exception exception)
        {
            Write("Warn\r\n" + message + "\r\n" + exception);
        }

        void Write(string msg)
        {
            System.Diagnostics.Trace.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff:"));
            System.Diagnostics.Trace.WriteLine(msg);
        }
    }
}

