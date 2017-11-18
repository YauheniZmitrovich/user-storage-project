using System.Collections.Generic;

namespace UserStorageServices.SerializationStrategies.Abstract
{
    public interface IUserSerializationStrategy
    {
        void SerializeUsers(HashSet<User> users, string fileName);

        HashSet<User> DeserializeUsers(string fileName);
    }
}
