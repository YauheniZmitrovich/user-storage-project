using UserStorageServices.Generators.Abstract;

namespace UserStorageServices.Generators.Concrete
{
    /// <summary>
    /// Generator of user id via <see cref="int"/>.
    /// </summary>
    public class UserIdGenerator : IUserIdGenerator
    {
        public UserIdGenerator(int lastId = -1)
        {
            LastId = lastId;
        }

        public int LastId { get; set; }

        public int Generate()
        {
            return LastId++;
        }
    }
}
