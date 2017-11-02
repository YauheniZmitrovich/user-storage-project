using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace UserStorageServices.Abstract
{
    /// <summary>
    /// Validator of user data.
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Validate user data
        /// </summary>
        void Validate(User user);
    }
}