using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Abstract;

namespace UserStorageServices.Concrete
{
    /// <summary>
    /// Generator of user id via <see cref="int"/>.
    /// </summary>
    public class UserIdGenerator : IUserIdGenerator
    {
        public int LastId { get; set; }

        public UserIdGenerator(int lastId = -1)
        {
            LastId = lastId;
        }

        public int Generate()
        {
            return LastId++;
        }
    }
}
