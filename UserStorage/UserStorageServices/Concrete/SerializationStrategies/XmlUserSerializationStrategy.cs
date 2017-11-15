using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UserStorageServices.Abstract;

namespace UserStorageServices.Concrete.SerializationStrategies
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