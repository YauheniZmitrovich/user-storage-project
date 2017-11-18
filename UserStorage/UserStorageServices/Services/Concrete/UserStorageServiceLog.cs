using System;
using System.Collections.Generic;
using System.Diagnostics;
using UserStorageServices.Enums;
using UserStorageServices.Services.Abstract;

namespace UserStorageServices.Services.Concrete
{
    /// <summary>
    /// Implementation of <see cref="UserStorageServiceDecorator"/>. Log all method calls.
    /// </summary>
    public class UserStorageServiceLog : UserStorageServiceDecorator
    {
        #region Fields

        /// <summary>
        /// Returns true if logging is enabled.
        /// </summary>
        private readonly BooleanSwitch _logging = new BooleanSwitch("enableLogging", "Switch in config file");

        #endregion

        #region Constructors and properties

        /// <summary>
        /// Create an instance of <see cref="UserStorageServiceLog"/>
        /// </summary>
        public UserStorageServiceLog(IUserStorageService storageService) : base(storageService) { }

        /// <summary>
        /// Gets the number of elements contained in the storage.
        /// </summary>
        /// <returns>An amount of users in the storage.</returns>
        public override int Count
        {
            get
            {
                LogIfEnabled("Count property is called.");

                return StorageService.Count;
            }
        }

        /// <summary>
        /// Mode of <see cref="UserStorageServiceMaster"/> work. 
        /// </summary>
        public override UserStorageServiceMode Mode
        {
            get
            {
                LogIfEnabled("Mode property is called");

                return StorageService.Mode;
            }
        }

        #endregion

        #region Public methods

        #region Add

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public override void Add(User user)
        {
            LogIfEnabled("Add(User user) method is called.");

            StorageService.Add(user);
        }

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        public override void Add(string firstName, string lastName, int age)
        {
            LogIfEnabled("Add(string firstName, string lastName, int age) method is called.");

            StorageService.Add(firstName, lastName, age);
        }

        #endregion

        #region Remove

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage by id.
        /// </summary>
        public override void Remove(int id)
        {
            LogIfEnabled("Remove(int id) method is called.");

            StorageService.Remove(id);
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public override void Remove(User user)
        {
            LogIfEnabled("Remove(User user) method is called.");

            StorageService.Remove(user);
        }

        #endregion

        #region Search

        #region returns IEnumerable<User>

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> by first name.
        /// </summary>
        public override IEnumerable<User> SearchByFirstName(string name)
        {
            LogIfEnabled("SearchByFirstName method is called.");

            return StorageService.SearchByFirstName(name);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> by last name.
        /// </summary>
        public override IEnumerable<User> SearchByLastName(string name)
        {
            LogIfEnabled("SearchByLastName method is called.");

            return StorageService.SearchByLastName(name);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> by age.
        /// </summary>
        public override IEnumerable<User> SearchByAge(int age)
        {
            LogIfEnabled("SearchByAge method is called.");

            return StorageService.SearchByAge(age);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> by predicate.
        /// </summary>
        public override IEnumerable<User> Search(Predicate<User> comparer)
        {
            LogIfEnabled("Search method is called.");

            return StorageService.Search(comparer);
        }

        #endregion

        #region returns User

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> by the first name.
        /// </summary>
        public override User FindFirstByFirstName(string name)
        {
            LogIfEnabled("FindFirstByFirstName method is called.");

            return StorageService.FindFirstByFirstName(name);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> by last name.
        /// </summary>
        public override User FindFirstByLastName(string name)
        {
            LogIfEnabled("FindFirstByLastName method is called.");

            return StorageService.FindFirstByLastName(name);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> by age.
        /// </summary>
        public override User FindFirstByAge(int age)
        {
            LogIfEnabled("FindFirstByAge method is called.");

            return StorageService.FindFirstByAge(age);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> by predicate.
        /// </summary>
        public override User FindFirst(Predicate<User> comparer)
        {
            LogIfEnabled("FindFirst method is called.");

            return FindFirst(comparer);
        }

        #endregion

        #endregion

        #endregion

        #region Private methods

        private void LogIfEnabled(string s)
        {
            if (_logging.Enabled)
            {
                Trace.TraceInformation(s);

                Trace.Flush();
            }
        }

        #endregion
    }
}
