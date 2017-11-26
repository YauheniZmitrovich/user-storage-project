using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using UserStorageServices.CustomAttributes;
using UserStorageServices.CustomExceptions;
using UserStorageServices.Validators.Abstract;

namespace UserStorageServices.Validators.Concrete
{
    [Serializable]
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
            PropertyInfo userAgeInfo = typeof(User).GetProperty("FirstName");

            var notNullAttribute = userAgeInfo?.GetCustomAttributes<ValidateNotNullOrEmptyAttribute>().FirstOrDefault();
            if (notNullAttribute != null && string.IsNullOrWhiteSpace(firstName))
                throw new FirstNameIsNullOrEmptyException("FirstName is null, empty or whitespace");

            var lengthAttribute = userAgeInfo?.GetCustomAttributes<ValidateMaxLengthAttribute>().FirstOrDefault();
            if (lengthAttribute != null && firstName.Length > lengthAttribute.MaxLength)
                throw new FirstNameTooLongException($"First name of user must be less than {lengthAttribute?.MaxLength}");

            var regexAttribute = userAgeInfo?.GetCustomAttributes<ValidateRegexAttribute>().FirstOrDefault();
            if (regexAttribute != null)
            {
                var regex = new Regex(regexAttribute.RegexString);
                if (!regex.IsMatch(firstName))
                    throw new FirstNameFormatException("Wrong format. Try using only letters");
            }
        }

        /// <summary>
        /// Validate user lastname.
        /// </summary>
        public void ValidateLastName(string lastName)
        {
            PropertyInfo userAgeInfo = typeof(User).GetProperty("LastName");

            var notNullAttribute = userAgeInfo?.GetCustomAttributes<ValidateNotNullOrEmptyAttribute>().FirstOrDefault();
            if (notNullAttribute != null && string.IsNullOrWhiteSpace(lastName))
                throw new LastNameIsNullOrEmptyException("LastName is null, empty or whitespace");

            var lengthAttribute = userAgeInfo?.GetCustomAttributes<ValidateMaxLengthAttribute>().FirstOrDefault();
            if (lengthAttribute != null && lastName.Length > lengthAttribute.MaxLength)
                throw new LastNameTooLongException($"Last name of user must be less than {lengthAttribute.MaxLength}");

            var regexAttribute = userAgeInfo?.GetCustomAttributes<ValidateRegexAttribute>().FirstOrDefault();
            if (regexAttribute != null)
            {
                 if (!Regex.IsMatch(lastName, regexAttribute.RegexString))
                    throw new LastNameFormatException("Wrong format. Try using only letters");
            }
        }

        /// <summary>
        /// Validate user age.
        /// </summary>
        public void ValidateAge(int age)
        {
            PropertyInfo userAgeInfo = typeof(User).GetProperty("Age");

            var minMaxAttribute = userAgeInfo?.GetCustomAttributes<ValidateMinMaxAttribute>().FirstOrDefault();
            if (minMaxAttribute != null)
                if (age < minMaxAttribute.Min || age > minMaxAttribute.Max)
                    throw new AgeExceedsLimitsException($"Age of user must be greater than {minMaxAttribute.Min} and less than {minMaxAttribute.Max}");
        }
    }
}
