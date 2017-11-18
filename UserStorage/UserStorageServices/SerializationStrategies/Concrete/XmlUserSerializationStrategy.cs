using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UserStorageServices.SerializationStrategies.Abstract;

namespace UserStorageServices.SerializationStrategies.Concrete
{
    public class XmlUserSerializationStrategy : IUserSerializationStrategy
    {
        public void SerializeUsers(HashSet<User> users, string fileName)
        {
            fileName = fileName + ".xml";

            var formatter = new XmlSerializer(typeof(HashSet<User>));

            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                formatter.Serialize(fs, users);
            }
        }

        public HashSet<User> DeserializeUsers(string fileName)
        {
            fileName = fileName + ".xml";

            var formatter = new XmlSerializer(typeof(HashSet<User>));

            HashSet<User> users;

            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                users = (HashSet<User>)formatter.Deserialize(fs);
            }

            return users;
        }
    }
}