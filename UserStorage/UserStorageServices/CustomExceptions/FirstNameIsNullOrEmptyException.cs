using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.CustomExceptions
{
    public class FirstNameIsNullOrEmptyException : Exception
    {
        public FirstNameIsNullOrEmptyException() { }

        public FirstNameIsNullOrEmptyException(string message) : base(message) { }

        public FirstNameIsNullOrEmptyException(string message, Exception inner) : base(message, inner) { }
    }
}
