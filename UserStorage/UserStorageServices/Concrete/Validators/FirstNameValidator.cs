using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Abstract;

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
                throw new ArgumentException("Firstname is null or empty or whitespace", nameof(user.FirstName));
            }
        }
    }
}
