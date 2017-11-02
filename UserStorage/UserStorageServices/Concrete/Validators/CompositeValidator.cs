using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Abstract;

namespace UserStorageServices.Concrete.Validators
{
    /// <summary>
    /// Validator of all user data.
    /// </summary>
    public class CompositeValidator : IValidator
    {
        private readonly IValidator[] _validators;

        /// <summary>
        /// Create instance of <see cref="CompositeValidator"/>
        /// </summary>
        public CompositeValidator()
        {
            _validators = new[]
            {
                (IValidator)new FirstNameValidator(),
                new LastNameValidator(),
                new AgeValidator()
            };
        }

        /// <summary>
        /// Validate all user data
        /// </summary>
        public void Validate(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            foreach (var item in _validators)
            {
                item.Validate(user);
            }
        }
    }
}
