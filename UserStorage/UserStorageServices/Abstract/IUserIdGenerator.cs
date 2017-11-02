using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Abstract
{
    public interface IUserIdGenerator
    {
        /// <summary>
        /// Generates an id for user.
        /// </summary>
        Guid Generate();
    }
}
