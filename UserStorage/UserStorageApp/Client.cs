using UserStorageServices;
using UserStorageServices.Abstract;
using UserStorageServices.Concrete;

namespace UserStorageApp
{
    /// <summary>
    /// Represents a client that uses an instance of the implementation of <see cref="UserStorageServiceBase"/>.
    /// </summary>
    public class Client
    {
        private readonly IUserStorageService _userStorageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client(IUserStorageService userStorageService)
        {
            _userStorageService = userStorageService;
        }

        /// <summary>
        /// Runs a sequence of actions on an instance of the implementation of <see cref="UserStorageServiceBase"/> class.
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
