namespace UserStorageServices.Validators.Abstract
{
    public interface IUserValidator
    {
        /// <summary>
        /// Validate all user data.
        /// </summary>
        void Validate(User user);

        /// <summary>
        /// Validate user first name.
        /// </summary>
        void ValidateFirstName(string name);

        /// <summary>
        /// Validate user last name.
        /// </summary>
        void ValidateLastName(string name);

        /// <summary>
        /// Validate user age.
        /// </summary>
        void ValidateAge(int age);
    }
}
