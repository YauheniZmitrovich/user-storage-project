using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UserStorageServices.Tests
{
    [TestClass]
    public class UserStorageServiceTests
    {
        #region Add tests

        #region Positive 

        [TestMethod]
        public void Add_UserParamAllOk()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(new User() { Age = 5, LastName = "Petrov", FirstName = "Vasya" });

            // Assert - [ExpectedException]
            Assert.AreEqual(1, userStorageService.Count);
        }

        [TestMethod]
        public void Add_ThreeParamsAllOk()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(age: 5, lastName: "Petrov", firstName: "Vasya");

            // Assert - [ExpectedException]
            Assert.AreEqual(1, userStorageService.Count);
        }

        #endregion

        #region Negative

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullAsUserArgument_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserFirstNameIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(new User
            {
                FirstName = null
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserLastNameIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(new User
            {
                LastName = null
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserAgeMoreThanTwoHundred_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(new User
            {
                Age = 201
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserAgeLessThanZero_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(new User
            {
                Age = 0
            });

            // Assert - [ExpectedException]
        }

        #endregion

        #endregion

        [TestMethod]
        public void Remove_WithoutArguments_NothingHappen()
        {
        }
    }
}
