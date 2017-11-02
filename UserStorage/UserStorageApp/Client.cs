using UserStorageServices;
using UserStorageServices.Concrete;

namespace UserStorageApp
{
    /// <summary>
    /// Represents a client that uses an instance of the <see cref="UserStorageService"/>.
    /// </summary>
    public class Client
    {
        private readonly UserStorageService _userStorageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client()
        {
            _userStorageService = new UserStorageService(new GuidUserIdGenerator());
        }

        /// <summary>
        /// Runs a sequence of actions on an instance of the <see cref="UserStorageService"/> class.
        /// </summary>
        public void Run()
        {
            User user = new User
            {
                FirstName = "Alex",
                LastName = "Black",
                Age = 25
            };

            _userStorageService.Add(user);

            _userStorageService.Remove(user);

            _userStorageService.SearchByFirstName("Alex");
        }
    }
}
