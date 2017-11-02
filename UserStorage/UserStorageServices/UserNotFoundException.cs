using System;

namespace UserStorageServices.CustomEvents
{
    /// <summary>
    /// Represents errors when user was not found. 
    /// </summary>
    public class UserNotFoundException : Exception
    {
        #region Constructors

        /// <summary>
        ///  Initializes a new instance of <see cref="UserNotFoundException"/> class with
        ///  message string with info about the user.
        /// </summary>
        /// <param name="user">The user which was not found. </param>
        public UserNotFoundException(User user) :
            base($"The user {user.FirstName + " " + user.LastName} was not founded.")
        {
            this.UnfoundUser = user;
        }

        /// <summary>
        ///  Initializes a new instance of <see cref="UserNotFoundException"/> class with
        ///  message string with info about the user.
        /// </summary>
        /// <param name="user">The books which was not founded. </param>
        /// <param name="message"> The message about exception. </param>
        public UserNotFoundException(string message, User user = null) : base(message)
        {
            this.UnfoundUser = user;
        }

        /// <summary>
        ///  Initializes a new instance of <see cref="UserNotFoundException"/> class with
        ///  message string with info about the user.
        /// </summary>
        /// <param name="user">The user which was not found. </param>
        /// <param name="message"> The message about exception. </param>
        /// <param name="innerException"> The inner exception. </param>
        public UserNotFoundException(User user, string message, Exception innerException) : base(message, innerException)
        {
            this.UnfoundUser = user;
        }

        #endregion

        #region Properties

        public User UnfoundUser { get; }

        #endregion
    }
}