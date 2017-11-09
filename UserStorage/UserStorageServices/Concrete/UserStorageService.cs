using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UserStorageServices.Abstract;
using UserStorageServices.Concrete.Validators;
using UserStorageServices.CustomExceptions;
using UserStorageServices.Enums;

namespace UserStorageServices.Concrete
{
    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public class UserStorageService : IUserStorageService
    {
        #region Fields

        /// <summary>
        /// Container for users.
        /// </summary>
        private readonly HashSet<User> _users;

        /// <summary>
        /// Generator of user id.
        /// </summary>
        private readonly IUserIdGenerator _userIdGenerator;

        /// <summary>
        /// Validator of user data.
        /// </summary>
        private readonly IUserValidator _validator;

        /// <summary>
        /// Services with slave mode.
        /// </summary>
        private readonly IList<IUserStorageService> _slaveServices;

        /// <summary>
        /// Mode of <see cref="UserStorageService"/> work. 
        /// </summary>
        private readonly UserStorageServiceMode _mode;

        /// <summary>
        /// Collection of subcribers.
        /// </summary>
        private readonly IList<INotificationSubscriber> _subscribers;

        #endregion

        #region Constructors and properties

        /// <summary>
        /// Create an instance of <see cref="UserStorageService"/>. 
        /// </summary>
        public UserStorageService(
            IUserIdGenerator idGenerator = null,
            IUserValidator validator = null,
            UserStorageServiceMode mode = UserStorageServiceMode.MasterNode,
            IEnumerable<IUserStorageService> slaveServices = null)
        {
            _users = new HashSet<User>();

            _userIdGenerator = idGenerator ?? new GuidUserIdGenerator();
            _validator = validator ?? new UserValidator();

            _mode = mode;
            if (mode == UserStorageServiceMode.MasterNode)
            {
                _slaveServices = slaveServices?.ToList() ?? new List<IUserStorageService>();
            }

            _subscribers = new List<INotificationSubscriber>();
        }

        /// <summary>
        /// Gets the number of elements contained in the storage.
        /// </summary>
        /// <returns>An amount of users in the storage.</returns>
        public int Count => _users.Count;

        #endregion

        #region Public methods

        #region Add

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public void Add(User user)
        {
            if (!IsAvailable())
            {
                throw new NotSupportedException();
            }

            _validator.Validate(user);

            user.Id = _userIdGenerator.Generate();
            _users.Add(user);

            foreach (var ob in _subscribers)
            {
                ob.UserAdded(user);
            }

            foreach (var service in _slaveServices)
            {
                service.Add(user);
            }
        }

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        public void Add(string firstName, string lastName, int age)
        {
            Add(new User() { Age = age, FirstName = firstName, LastName = lastName });
        }

        #endregion

        #region Remove

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage by id.
        /// </summary>
        public void Remove(Guid id)
        {
            if (!IsAvailable())
            {
                throw new NotSupportedException();
            }

            int number = _users.RemoveWhere(x => x.Id == id);
            if (number == 0)
            {
                throw new UserNotFoundException("The user was not found");
            }

            foreach (var ob in _subscribers)
            {
                ob.UserRemoved(_users.First(x => x.Id == id));
            }

            foreach (var service in _slaveServices)
            {
                service.Remove(id);
            }
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public void Remove(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            try
            {
                Remove(user.Id);
            }
            catch (UserNotFoundException exc)
            {
                throw new UserNotFoundException(exc.Message, user);
            }
        }

        #endregion

        #region Search

        #region returns IEnumerable<User>

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> by first name.
        /// </summary>
        public IEnumerable<User> SearchByFirstName(string name)
        {
            _validator.ValidateFirstName(name);

            return Search(u => u.FirstName == name);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> by last name.
        /// </summary>
        public IEnumerable<User> SearchByLastName(string name)
        {
            _validator.ValidateLastName(name);

            return Search(u => u.LastName == name);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> by age.
        /// </summary>
        public IEnumerable<User> SearchByAge(int age)
        {
            _validator.ValidateAge(age);

            return Search(u => u.Age == age);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> by predicate.
        /// </summary>
        public IEnumerable<User> Search(Predicate<User> comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return _users.Select(u => u).Where(u => comparer(u));
        }

        #endregion

        #region returns User

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> by the first name.
        /// </summary>
        public User FindFirstByFirstName(string name)
        {
            _validator.ValidateFirstName(name);

            return FindFirst(u => u.FirstName == name);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> by last name.
        /// </summary>
        public User FindFirstByLastName(string name)
        {
            _validator.ValidateLastName(name);

            return FindFirst(u => u.LastName == name);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> by age.
        /// </summary>
        public User FindFirstByAge(int age)
        {
            _validator.ValidateAge(age);

            return FindFirst(u => u.Age == age);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> by predicate.
        /// </summary>
        public User FindFirst(Predicate<User> comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return _users.First(u => comparer(u));
        }

        #endregion

        #endregion

        #region Subscriber magement

        public void AddSubscriber(INotificationSubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void RemoveSubscriber(INotificationSubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
        }

        #endregion

        #endregion

        #region Private methods

        private bool IsAvailable()
        {
            if (_mode == UserStorageServiceMode.MasterNode)
            {
                return true;
            }

            var stackTrace = new StackTrace();
            var currentCalled = stackTrace.GetFrame(1).GetMethod();
            var flag = (stackTrace.GetFrames() ?? throw new InvalidOperationException())
                .Select(x => x.GetMethod())
                .Count(x => x == currentCalled);

            return flag >= 2;
        }

        #endregion
    }
}
