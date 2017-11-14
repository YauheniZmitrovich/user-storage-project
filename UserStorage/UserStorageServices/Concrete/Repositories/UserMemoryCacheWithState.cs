using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UserStorageServices.Abstract;

namespace UserStorageServices.Concrete.Repositories
{
    public class UserMemoryCacheWithState : UserMemoryCache
    {
        private readonly string _fileName;

        public UserMemoryCacheWithState(IUserIdGenerator generator = null, string path = null) : base(generator)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = "repository.bin";
            }

            _fileName = path;
        }

        public override void Start()
        {
            var formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(_fileName, FileMode.Open))
            {
                Users = (HashSet<User>) formatter.Deserialize(fs);
            }
        }

        public override void Stop()
        {
            var formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(_fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Users);
            }
        }
    }
}

