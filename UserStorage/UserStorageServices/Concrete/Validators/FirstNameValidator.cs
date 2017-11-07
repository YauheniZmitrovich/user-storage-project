using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Abstract;
using UserStorageServices.CustomExceptions;

namespace UserStorageServices.Concrete.Validators
{
    /// <summary>
    /// Validator of user firstname.
    /// </summary>
    public class FirstNameValidator : IValidator
    {
        /// <summary>
        /// Validate user firstname.
        /// </summary>
        public void Validate(User user)
        {
            if (string.IsNullOrWhiteSpace(user.FirstName))
            {
                throw new FirstNameIsNullOrEmptyException("Firstname is null or empty or whitespace");
            }
        }
    }
}
