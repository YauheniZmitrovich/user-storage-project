using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Abstract;

namespace UserStorageServices.Concrete.Validators
{
    /// <summary>
    /// Validator of user age.
    /// </summary>
    public class AgeValidator : IValidator
    {
        /// <summary>
        /// Validate user age.
        /// </summary>
        public void Validate(User user)
        {
            if (user.Age < 1 || user.Age > 200)
            {
                throw new ArgumentException("Age have to be more than zero and less than 200", nameof(user));
            }
        }
    }
}
