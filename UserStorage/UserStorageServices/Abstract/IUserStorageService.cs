using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Enums;

namespace UserStorageServices.Abstract
{
    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public interface IUserStorageService
    {
        #region Properties

        /// <summary>
        /// Gets the number of elements contained in the storage.
        /// </summary>
        /// <returns>An amount of users in the storage.</returns>
        int Count { get; }

        /// <summary>
        /// Mode of <see cref="IUserStorageService"/> work. 
        /// </summary>
        UserStorageServiceMode Mode { get; }

        #endregion

        #region Public methods

        #region Add

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        void Add(User user);

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        void Add(string firstName, string lastName, int age);

        #endregion

        #region Remove

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage by id.
        /// </summary>
        void Remove(int id);

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        void Remove(User user);

        #endregion

        #region Search

        #region returns IEnumerable<User>

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> by first name.
        /// </summary>
        IEnumerable<User> SearchByFirstName(string name);

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> by last name.
        /// </summary>
        IEnumerable<User> SearchByLastName(string name);

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> by age.
        /// </summary>
        IEnumerable<User> SearchByAge(int age);

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> by predicate.
        /// </summary>
        IEnumerable<User> Search(Predicate<User> comparer);
        #endregion

        #region returns User

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> by the first name.
        /// </summary>
        User FindFirstByFirstName(string name);

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> by last name.
        /// </summary>
        User FindFirstByLastName(string name);

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> by age.
        /// </summary>
        User FindFirstByAge(int age);

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> by predicate.
        /// </summary>
        User FindFirst(Predicate<User> comparer);

        #endregion

        #endregion

        #endregion
    }
}
