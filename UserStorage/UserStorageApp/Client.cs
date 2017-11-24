using System;
using System.Configuration;
using System.ServiceModel;
using UserStorageServices;
using UserStorageServices.Repositories.Abstract;
using UserStorageServices.Repositories.Concrete;
using UserStorageServices.Services.Abstract;
using UserStorageServices.Services.Concrete;
using ServiceConfiguration = ServiceConfigurationSection.ServiceConfigurationSection;

namespace UserStorageApp
{
    /// <summary>
    /// Represents a client that uses an instance of the implementation of <see cref="UserStorageServiceBase"/>.
    /// </summary>
    public class Client
    {
        private readonly IUserStorageService _userStorageService;

        private readonly IUserRepositoryManager _repositoryManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client(IUserStorageService userStorageService, IUserRepositoryManager repository)
        {
            _repositoryManager = repository ?? throw new ArgumentNullException(nameof(repository));

            _userStorageService = userStorageService ?? throw new ArgumentNullException(nameof(userStorageService));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client()
        {
            var path = ReadSetting("FilePath");

            _repositoryManager = new UserFileRepository(path: path);

            _userStorageService = new UserStorageServiceLog(new UserStorageServiceMaster((IUserRepository)_repositoryManager));
        }

        /// <summary>
        /// Runs a sequence of actions on an instance of the implementation of <see cref="UserStorageServiceBase"/> class.
        /// </summary>
        public void Run()
        {
            var serviceConfiguration = (ServiceConfiguration)System.Configuration.ConfigurationManager.GetSection("serviceConfiguration");

            User user = new User
            {
                FirstName = "Vitya",
                LastName = "Stepanov",
                Age = 20
            };

            _repositoryManager.Start();

            _userStorageService.Add(user);

            _userStorageService.Remove(user);

            _userStorageService.Add(user);

            _repositoryManager.Stop();
        }

        private static string ReadSetting(string key)
        {
            var appSettings = ConfigurationManager.AppSettings;

            return appSettings[key] ?? "NotFound";
        }
    }
}
