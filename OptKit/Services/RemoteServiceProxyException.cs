using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace OptKit.Services
{
    [Serializable]
    public class RemoteServiceProxyException : AppException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteServiceProxyException"/> class.
        /// </summary>
        public RemoteServiceProxyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteServiceProxyException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error. </param>
        public RemoteServiceProxyException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteServiceProxyException"/> class.
        /// </summary>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception. If the innerException parameter 
        /// is not a null reference, the current exception is raised in a catch block that handles 
        /// the inner exception.
        /// </param>
        public RemoteServiceProxyException(Exception innerException) : base(innerException.Message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteServiceProxyException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error. </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception. If the innerException parameter 
        /// is not a null reference, the current exception is raised in a catch block that handles 
        /// the inner exception.
        /// </param>
        public RemoteServiceProxyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteServiceProxyException"/> class 
        /// with serialized data.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> that holds the serialized object 
        /// data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext"/> that contains contextual information about the source or destination.
        /// </param>
        protected RemoteServiceProxyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}