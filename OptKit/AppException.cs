using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace OptKit
{
    /// <summary>
    /// 提供应用程序异常
    /// </summary>
    [Serializable]
    public class AppException : ApplicationException
    {
        /// <summary>
        /// 构造<see cref="AppException"/>实例
        /// </summary>
        public AppException() { }

        /// <summary>
        /// 使用指定的信息构造<see cref="AppException"/>实例
        /// </summary>
        /// <param name="message">异常信息</param>
        public AppException(string message) : base(message) { }

        /// <summary>
        /// 使用指定的信息和相关的异常构造<see cref="AppException"/>实例
        /// </summary>
        /// <param name="message">异常信息</param>
        /// <param name="innerException">相关异常</param>
        public AppException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// 使用序列化数据构造<see cref="AppException"/>实例
        /// </summary>
        /// <param name="info">序列化信息</param>
        /// <param name="context">序列化流上下文</param>
        protected AppException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
