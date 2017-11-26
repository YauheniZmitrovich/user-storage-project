using System;
using System.Runtime.Serialization;

namespace UserStorageServices.CustomExceptions
{
    [Serializable]
    public class LastNameTooLongException : Exception
    {
        public LastNameTooLongException() : base("Last name is too long.")
        {
        }

        public LastNameTooLongException(string message) : base(message)
        {
        }

        public LastNameTooLongException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LastNameTooLongException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}