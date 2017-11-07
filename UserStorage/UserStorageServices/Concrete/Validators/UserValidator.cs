using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Abstract;
using UserStorageServices.CustomExceptions;

namespace UserStorageServices.Concrete.Validators
{
    public class UserValidator : IUserValidator
    {
        /// <summary>
        /// Validate all user data.
        /// </summary>
        public void Validate(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            ValidateFirstName(user.FirstName);
            ValidateLastName(user.LastName);
            ValidateAge(user.Age);
        }

        /// <summary>
        /// Validate user firstname.
        /// </summary>
        public void ValidateFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new FirstNameIsNullOrEmptyException("Firstname is null or empty or whitespace");
            }
        }

        /// <summary>
        /// Validate user lastname.
        /// </summary>
        public void ValidateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new LastNameIsNullOrEmptyException("Lastname is null or empty or whitespace");
            }
        }

        /// <summary>
        /// Validate user age.
        /// </summary>
        public void ValidateAge(int age)
        {
            if (age < 1 || age > 200)
            {
                throw new AgeExceedsLimitsException("Age have to be more than zero and less than 200");
            }
        }
    }
}
