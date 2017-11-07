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
    /// Validator of user lastname.
    /// </summary>
    public class LastNameValidator : IValidator
    {
        /// <summary>
        /// Validate user lastname.
        /// </summary>
        public void Validate(User user)
        {
            if (string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new LastNameIsNullOrEmptyException("Lastname is null, empty or whitespace");
            }
        }
    }
}
