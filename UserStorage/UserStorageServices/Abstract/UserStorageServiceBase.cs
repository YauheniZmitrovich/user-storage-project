using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UserStorageServices.Abstract;
using UserStorageServices.Concrete;
using UserStorageServices.Concrete.Validators;
using UserStorageServices.CustomExceptions;
using UserStorageServices.Enums;

namespace UserStorageServices.Abstract
{
    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public abstract class UserStorageServiceBase : IUserStorageService
    {
        #region Fields

        /// <summary>
        /// Container for users.
        /// </summary>
        protected readonly HashSet<User> Users;

        /// <summary>
        /// Generator of user id.
        /// </summary>
        private readonly IUserIdGenerator _userIdGenerator;

        /// <summary>
        /// Validator of user data.
        /// </summary>
        private readonly IUserValidator _validator;

        #endregion

        #region Constructors and properties

        /// <summary>
        /// Create an instance of <see cref="UserStorageServiceBase"/>. 
        /// </summary>
        protected UserStorageServiceBase(
            IUserIdGenerator idGenerator = null,
            IUserValidator validator = null
            )
        {
            Users = new HashSet<User>();

            _userIdGenerator = idGenerator ?? new GuidUserIdGenerator();
            _validator = validator ?? new UserValidator();
        }

        /// <summary>
        /// Gets the number of elements contained in the storage.
        /// </summary>
        /// <returns>An amount of users in the storage.</returns>
        public int Count => Users.Count;

        /// <summary>
        /// Mode of <see cref="UserStorageServiceBase"/> work. 
        /// </summary>
        public abstract UserStorageServiceMode Mode { get; }

        #endregion

        #region Public methods

        #region Add

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public virtual void Add(User user)
        {
            _validator.Validate(user);

            user.Id = _userIdGenerator.Generate();

            Users.Add(user);
        }

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        public virtual void Add(string firstName, string lastName, int age)
        {
            Add(new User() { Age = age, FirstName = firstName, LastName = lastName });
        }

        #endregion

        #region Remove

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage by id.
        /// </summary>
        public virtual void Remove(Guid id)
        {
            int number = Users.RemoveWhere(x => x.Id == id);

            if (number == 0)
            {
                throw new UserNotFoundException("The user was not found");
            }
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public virtual void Remove(User user)
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

            return Users.Select(u => u).Where(u => comparer(u));
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

            return Users.FirstOrDefault(u => comparer(u));
        }

        #endregion

        #endregion

        #endregion
    }
}