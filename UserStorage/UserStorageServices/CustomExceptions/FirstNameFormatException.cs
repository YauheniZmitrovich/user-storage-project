using System;
using System.Runtime.Serialization;

namespace UserStorageServices.CustomExceptions
{
    [Serializable]
    public class FirstNameFormatException : Exception
    {
        public FirstNameFormatException()
        {
        }

        public FirstNameFormatException(string message) : base(message)
        {
        }

        public FirstNameFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FirstNameFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}