using System;
using System.Runtime.Serialization;

namespace TkwEF.BLL
{
    public class BllException : Exception
    {
        public BllException()
        {
        }

        public BllException(string message) : base(message)
        {
        }

        public BllException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BllException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
