using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UserStorageServices.Concrete.Repositories
{
    public class UserMemoryCacheWithState : UserMemoryCache
    {
        public override void Start()
        {
            var formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("repository.bin", FileMode.Open))
            {
                Users = (HashSet<User>)formatter.Deserialize(fs);
            }
        }
        public override void Stop()
        {
            var formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("repository.bin", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Users);
            }
        }
    }
}

