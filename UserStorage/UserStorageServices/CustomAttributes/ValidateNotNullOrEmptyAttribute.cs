using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.CustomExceptions;

namespace UserStorageServices.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateNotNullOrEmptyAttribute : Attribute
    {
    }
}
