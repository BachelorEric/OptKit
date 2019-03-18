using System;

namespace OptKit.Logging
{
    /// <summary>
	/// A simple logging interface abstracting logging APIs. This interface will not registor to IoC.
	/// </summary>
    /// <remarks>
    /// <para>
    /// Implementations should defer calling a message's <see cref="object.ToString()"/> until the message really needs
    /// to be logged to avoid performance penalties.
    /// </para>
    /// <para>
    /// Using this style has the advantage to defer possibly expensive message argument evaluation and formatting (and formatting arguments!) until the message gets
    /// actually logged. If the message is not logged at all (e.g. due to <see cref="LogLevel"/> settings), 
    /// you won't have to pay the peformance penalty of creating the message.
    /// </para>
    /// </remarks>
    /// <example>
    /// The example below demonstrates using callback style for creating the message, where the call to the 
    /// <see cref="Random.NextDouble"/> and the underlying <see cref="string.Format(string,object[])"/> only happens, if level Debug is enabled:
    /// <code>
    /// Log.Debug( m=&gt;m(&quot;result is {0}&quot;, random.NextDouble()) );
    /// Log.Debug(delegate(m) { m(&quot;result is {0}&quot;, random.NextDouble()); });
    /// </code>
    /// </example>
    /// <author>Mark Pollack</author>
    /// <author>Bruno Baia</author>
    /// <author>Erich Eichinger</author>
    public interface ILog
    {
        /// <summary>
        /// Log a message object with the Debug level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        void Debug(object message);

        /// <summary>
        /// Log a message object with the Debug level including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        void Debug(object message, Exception exception);

        /// <summary>
        /// Log a message object with the Info level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        void Info(object message);

        /// <summary>
        /// Log a message object with the Info level including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        void Info(object message, Exception exception);

        /// <summary>
        /// Log a message object with the Warn level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        void Warn(object message);

        /// <summary>
        /// Log a message object with the Warn level including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        void Warn(object message, Exception exception);

        /// <summary>
        /// Log a message object with the Error level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        void Error(object message);

        /// <summary>
        /// Log a message object with the Error level including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        void Error(object message, Exception exception);

        /// <summary>
        /// Log a message object with the Fatal level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        void Fatal(object message);

        /// <summary>
        /// Log a message object with the Fatal level including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        void Fatal(object message, Exception exception);

        /// <summary>
        /// Checks if this logger is enabled for the Debug level.
        /// </summary>
        bool IsDebugEnabled { get; }

        /// <summary>
        /// Checks if this logger is enabled for the Error level.
        /// </summary>
        bool IsErrorEnabled { get; }

        /// <summary>
        /// Checks if this logger is enabled for the Fatal level.
        /// </summary>
        bool IsFatalEnabled { get; }

        /// <summary>
        /// Checks if this logger is enabled for the Info level.
        /// </summary>
        bool IsInfoEnabled { get; }

        /// <summary>
        /// Checks if this logger is enabled for the Warn level.
        /// </summary>
        bool IsWarnEnabled { get; }
    }
}
