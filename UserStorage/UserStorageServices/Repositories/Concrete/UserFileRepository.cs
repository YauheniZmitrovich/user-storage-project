using System;
using System.Linq;
using UserStorageServices.Generators.Abstract;
using UserStorageServices.Repositories.Abstract;
using UserStorageServices.SerializationStrategies.Abstract;
using UserStorageServices.SerializationStrategies.Concrete;

namespace UserStorageServices.Repositories.Concrete
{
    [Serializable]
    public class UserFileRepository : UserRepository, IUserRepositoryManager
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

        public void Start()
        {
            Users = _serializationStrategy.DeserializeUsers(_fileName);

            IdGenerator.LastId = Users.Max(u => u.Id);
        }

        public void Stop()
        {
            _serializationStrategy.SerializeUsers(Users, _fileName);
        }
    }
}