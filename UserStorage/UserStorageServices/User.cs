using System;

namespace UserStorageServices
{
    /// <summary>
    /// Represents a user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Generates and gets an user id.
        /// </summary>
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets a user first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets a user last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets a user age.
        /// </summary>
        public int Age { get; set; }
    }
}
