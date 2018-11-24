using System;
using System.Runtime.Serialization;

namespace OptKit.Wpf.UI
{
    [Serializable]
    public class MahAppsException : Exception
    {
        public MahAppsException()
        {
        }

        public MahAppsException(string message)
            : base(message)
        {
        }

        public MahAppsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected MahAppsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}