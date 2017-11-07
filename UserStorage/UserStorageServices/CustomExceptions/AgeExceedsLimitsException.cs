using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.CustomExceptions
{
    public class AgeExceedsLimitsException : Exception
    {
        public AgeExceedsLimitsException() { }

        public AgeExceedsLimitsException(string message) : base(message) { }

        public AgeExceedsLimitsException(string message, Exception inner) : base(message, inner) { }
    }
}