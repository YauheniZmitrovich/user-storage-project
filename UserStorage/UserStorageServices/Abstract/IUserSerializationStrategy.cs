using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Abstract
{
    public interface IUserSerializationStrategy
    {
        void SerializeUsers(HashSet<User> users, string fileName);

        HashSet<User> DeserializeUsers(string fileName);
    }
}
