using System;
using System.Collections.Generic;

namespace UserStorageServices
{
    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public class UserStorageService
    {
        #region Fields

        /// <summary>
        /// Container for users.
        /// </summary>
        private readonly HashSet<User> _users;

        #endregion

        #region Constructors and properties

        /// <summary>
        /// Create an instance of <see cref="UserStorageService"/>. 
        /// </summary>
        public UserStorageService()
        {
            _users = new HashSet<User>();
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
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            CheckInputName(user.FirstName);
            CheckInputName(user.LastName);

            if (user.Age < 1 || user.Age > 200)
            {
                   throw new ArgumentException("Age have to be more than zero and less than 200", nameof(user));
            }

            _users.Add(user);
        }

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        public void Add(string firstName, string lastName, int age)
        {
            Add(new User() { Age = age, FirstName = firstName, LastName = lastName });
        }

        #endregion

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public void Remove()
        {
            // TODO: Implement Remove() method.
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> that matches specified criteria.
        /// </summary>
        public void Search()
        {
            // TODO: Implement Search() method.
        }

        #endregion

        #region Private methods

        private void CheckInputName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Input name is null or empty or whitespace", nameof(name));
            }
        }

        #endregion
    }
}
