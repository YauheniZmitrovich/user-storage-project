using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UserStorageServices.Abstract;
using UserStorageServices.Concrete.SerializationStrategies;

namespace UserStorageServices.Concrete.Repositories
{
    public class UserFileRepository : UserRepository
    {
        private readonly string _fileName;

        private readonly IUserSerializationStrategy _serializationStrategy;

        public UserFileRepository(
            IUserIdGenerator generator = null,
            IUserSerializationStrategy serializationStrategy = null,
            string path = null) : base(generator)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                path = "repository";
            }

            _serializationStrategy = serializationStrategy ?? new BinaryUserSerializationStrategy();

            _fileName = path;
        }

        public override void Start()
        {
            Users = _serializationStrategy.DeserializeUsers(_fileName);

            IdGenerator.LastId = Users.Max(u => u.Id);
        }

        public override void Stop()
        {
            _serializationStrategy.SerializeUsers(Users, _fileName);
        }
    }
}