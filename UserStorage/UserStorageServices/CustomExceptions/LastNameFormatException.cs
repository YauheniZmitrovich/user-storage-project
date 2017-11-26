using System;
using System.Runtime.Serialization;

namespace UserStorageServices.CustomExceptions
{
    [Serializable]
    public class LastNameFormatException : Exception
    {
        public LastNameFormatException()
        {
        }

        public LastNameFormatException(string message) : base(message)
        {
        }

        public LastNameFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LastNameFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}