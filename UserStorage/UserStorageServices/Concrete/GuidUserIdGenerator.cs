using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Abstract;

namespace UserStorageServices.Concrete
{
    /// <summary>
    /// Generator of user id via <see cref="Guid"/>.
    /// </summary>
    public class GuidUserIdGenerator : IUserIdGenerator
    {
        /// <summary>
        /// Generates an id for user via <see cref="Guid"/>.
        /// </summary>
        public Guid Generate()
        {
            return Guid.NewGuid();
        }
    }
}
