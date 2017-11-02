using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Abstract;

namespace UserStorageServices.Concrete
{
    /// <summary>
    /// Validator of all user data.
    /// </summary>
    public class UserValidator : IUserValidator
    {
        /// <summary>
        /// Validate all user data
        /// </summary>
        public void Validate(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(user.FirstName))
            {
                throw new ArgumentException("Firstname is null or empty or whitespace", nameof(user.FirstName));
            }

            if (string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new ArgumentException("Lastname is null, empty or whitespace", nameof(user));
            }

            if (user.Age < 1 || user.Age > 200)
            {
                throw new ArgumentException("Age have to be more than zero and less than 200", nameof(user));
            }
        }
    }
}
