using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace OptKit.Data
{
    /// <summary>
    /// 数据访问的异常
    /// </summary>
    [Serializable]
    public class DataException : AppException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataException"/> class.
        /// </summary>
        public DataException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error. </param>
        public DataException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataException"/> class.
        /// </summary>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception. If the innerException parameter 
        /// is not a null reference, the current exception is raised in a catch block that handles 
        /// the inner exception.
        /// </param>
        public DataException(Exception innerException) : base(innerException.Message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error. </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception. If the innerException parameter 
        /// is not a null reference, the current exception is raised in a catch block that handles 
        /// the inner exception.
        /// </param>
        public DataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataException"/> class 
        /// with serialized data.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> that holds the serialized object 
        /// data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext"/> that contains contextual information about the source or destination.
        /// </param>
        protected DataException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}