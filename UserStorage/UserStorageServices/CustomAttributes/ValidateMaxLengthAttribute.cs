using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.CustomExceptions;
using UserStorageServices.Validators.Abstract;

namespace UserStorageServices.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateMaxLengthAttribute : Attribute
    {
        public ValidateMaxLengthAttribute(int maxLength = 50)
        {
            MaxLength = maxLength;
        }

        public int MaxLength { get; }
    }
}
