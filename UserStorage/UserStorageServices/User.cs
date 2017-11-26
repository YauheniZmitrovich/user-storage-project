using System;
using System.Collections.Generic;
using UserStorageServices.CustomAttributes;

namespace UserStorageServices
{
    /// <summary>
    /// Represents a user.
    /// </summary>
    [Serializable]
    public class User : IEquatable<User>
    {
        #region Properties

        /// <summary>
        /// Generates and gets an user id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a user first name.
        /// </summary>
        [ValidateMaxLength(20)]
        [ValidateNotNullOrEmpty]
        [ValidateRegex("([A-Za-z])\\w+")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets a user last name.
        /// </summary>
        [ValidateMaxLength(25)]
        [ValidateNotNullOrEmpty]
        [ValidateRegex("([A-Za-z])\\w+")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets a user age.
        /// </summary>
        [ValidateMinMax(18, 110)]
        public int Age { get; set; }

        #endregion

        #region Equality comparison

        public static bool operator ==(User lhs, User rhs)
        {
            if (ReferenceEquals(lhs, rhs))
            {
                return true;
            }

            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
            {
                return false;
            }

            return lhs.Equals(rhs);
        }

        public static bool operator !=(User lhs, User rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(User other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((User)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion
    }
}
