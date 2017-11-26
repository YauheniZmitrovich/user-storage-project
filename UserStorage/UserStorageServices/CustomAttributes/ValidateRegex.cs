using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UserStorageServices.CustomExceptions;
using UserStorageServices.Validators.Abstract;

namespace UserStorageServices.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateRegexAttribute : Attribute
    {
        public ValidateRegexAttribute(string regexString)
        {
            RegexString = regexString;
        }

        public string RegexString { get; }
    }
}
