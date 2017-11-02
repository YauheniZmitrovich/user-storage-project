using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace UserStorageServices.Abstract
{
    /// <summary>
    /// Validator of all user data.
    /// </summary>
    public interface IUserValidator
    {
        /// <summary>
        /// Validate all user data
        /// </summary>
        void Validate(User user);
    }
}