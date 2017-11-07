using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.CustomExceptions
{
    public class LastNameIsNullOrEmptyException : Exception
    {
        public LastNameIsNullOrEmptyException() { }

        public LastNameIsNullOrEmptyException(string message) : base(message) { }

        public LastNameIsNullOrEmptyException(string message, Exception inner) : base(message, inner) { }
    }
}
