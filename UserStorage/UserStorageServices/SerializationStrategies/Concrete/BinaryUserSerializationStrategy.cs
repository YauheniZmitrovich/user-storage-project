using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UserStorageServices.SerializationStrategies.Abstract;

namespace UserStorageServices.SerializationStrategies.Concrete
{
    public class BinaryUserSerializationStrategy : IUserSerializationStrategy
    {
        public void SerializeUsers(HashSet<User> users, string fileName)
        {
            fileName = fileName + ".bin";

            var formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                formatter.Serialize(fs, users);
            }
        }

        public HashSet<User> DeserializeUsers(string fileName)
        {
            fileName = fileName + ".bin";

            var formatter = new BinaryFormatter();

            HashSet<User> users;

            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                users = (HashSet<User>)formatter.Deserialize(fs);
            }

            return users;
        }
    }
}
