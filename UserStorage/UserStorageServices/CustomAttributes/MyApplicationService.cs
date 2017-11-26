using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MyApplicationServiceAttribute : Attribute
    {
        public MyApplicationServiceAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
