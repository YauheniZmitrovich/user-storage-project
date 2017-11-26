using System;
using System.Runtime.Serialization;

namespace UserStorageServices.CustomExceptions
{
    [Serializable]
    public class FirstNameTooLongException : Exception
    {
        public FirstNameTooLongException() : base("First name is too long.")
        {
        }

        public FirstNameTooLongException(string message) : base(message)
        {
        }

        public FirstNameTooLongException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FirstNameTooLongException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}