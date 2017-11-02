using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserStorageServices.Concrete;
using UserStorageServices.CustomExceptions;

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
            var userStorageService = new UserStorageService(new GuidUserIdGenerator(), new UserValidator());

            // Act
            userStorageService.Add(new User() { Age = 5, LastName = "Petrov", FirstName = "Vasya" });

            // Assert - [ExpectedException]
            Assert.AreEqual(1, userStorageService.Count);
        }

        [TestMethod]
        public void Add_ThreeParamsAllOk()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidUserIdGenerator(), new UserValidator());

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
            var userStorageService = new UserStorageService(new GuidUserIdGenerator(), new UserValidator());

            // Act
            userStorageService.Add(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserFirstNameIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidUserIdGenerator(), new UserValidator());

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
            var userStorageService = new UserStorageService(new GuidUserIdGenerator(), new UserValidator());

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
            var userStorageService = new UserStorageService(new GuidUserIdGenerator(), new UserValidator());

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
            var userStorageService = new UserStorageService(new GuidUserIdGenerator(), new UserValidator());

            // Act
            userStorageService.Add(new User
            {
                Age = 0
            });

            // Assert - [ExpectedException]
        }

        #endregion

        #endregion

        #region Remove tests

        #region Positive

        [TestMethod]
        public void Remove_ById_AllOk()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidUserIdGenerator(), new UserValidator());

            // Act
            var vasya = new User() { Age = 5, LastName = "Petrov", FirstName = "Vasya" };
            userStorageService.Add(vasya);
            userStorageService.Remove(vasya.Id);

            // Assert - [ExpectedException]
            Assert.AreEqual(0, userStorageService.Count);
        }

        [TestMethod]
        public void Remove_ByUserOb_AllOk()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidUserIdGenerator(), new UserValidator());

            // Act
            var vasya = new User() { Age = 5, LastName = "Petrov", FirstName = "Vasya" };
            userStorageService.Add(vasya);
            userStorageService.Remove(vasya);

            // Assert - [ExpectedException]
            Assert.AreEqual(0, userStorageService.Count);
        }

        #endregion

        #region Negative

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Remove_NullAsUserArgument_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidUserIdGenerator(), new UserValidator());

            // Act
            userStorageService.Remove(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void Remove_UserNotFound_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidUserIdGenerator(), new UserValidator());

            // Act
            userStorageService.Remove(new User() { FirstName = "Vasya" });

            // Assert - [ExpectedException]
        }

        #endregion

        #endregion

        #region Search tests

        #region Positive

        #region Returns first user

        [TestMethod]
        public void FindFirstByFirstName_AllOk()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidUserIdGenerator(), new UserValidator());

            var user = new User()
            {
                LastName = "Kudosh",
                FirstName = "Andrew",
                Age = 20
            };
            userStorageService.Add(user);

            // Act 
            var result = userStorageService.FindFirstByFirstName("Andrew");

            // Assert
            Assert.AreEqual(user.Age, result.Age);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
        }

        [TestMethod]
        public void FindFirstByLastName_AllOk()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidUserIdGenerator(), new UserValidator());

            var user = new User()
            {
                LastName = "Kudosh",
                FirstName = "Andrew",
                Age = 20
            };
            userStorageService.Add(user);

            // Act 
            var result = userStorageService.FindFirstByLastName("Kudosh");

            // Assert
            Assert.AreEqual(user.Age, result.Age);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
        }

        [TestMethod]
        public void FindFirstByAge_AllOk()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidUserIdGenerator(), new UserValidator());

            var user = new User()
            {
                LastName = "Kudosh",
                FirstName = "Andrew",
                Age = 20
            };
            userStorageService.Add(user);

            // Act 
            var result = userStorageService.FindFirstByAge(20);

            // Assert
            Assert.AreEqual(user.Age, result.Age);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
        }

        #endregion

        #region Returns IEnumerable<User>

        [TestMethod]
        public void SearchByFirstName_AllOk()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidUserIdGenerator(), new UserValidator());
            var user1 = new User() { Age = 5, LastName = "Petrov", FirstName = "Vasya" };

            userStorageService.Add(new User() { Age = 5, LastName = "Petrov", FirstName = "Vasya" });
            userStorageService.Add(user1);

            // Act 
            var result = userStorageService.SearchByFirstName("Vasya");

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void SearchByLastName_AllOk()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidUserIdGenerator(), new UserValidator());
            var user1 = new User() { Age = 5, LastName = "Petrov", FirstName = "Vasya" };

            userStorageService.Add(new User() { Age = 5, LastName = "Petrov", FirstName = "Vasya" });
            userStorageService.Add(user1);

            // Act 
            var result = userStorageService.SearchByLastName("Petrov");

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void SearchByAge_AllOk()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidUserIdGenerator(), new UserValidator());
            var user1 = new User() { Age = 5, LastName = "Petrov", FirstName = "Vasya" };

            userStorageService.Add(new User() { Age = 5, LastName = "Petrov", FirstName = "Vasya" });
            userStorageService.Add(user1);

            // Act 
            var result = userStorageService.SearchByAge(5);

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion

        #endregion

        #region Negative

        [ExpectedException(typeof(ArgumentException))]
        public void FindFirstByFirstName_EmptyName_ExceptionThrow()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidUserIdGenerator(), new UserValidator());

            // Act
            userStorageService.FindFirstByFirstName(string.Empty);

            // Assert - [ExpectedException] 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FindFirstByLastName_WhiteSpace_ExceptionThrow()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidUserIdGenerator(), new UserValidator());

            // Act
            userStorageService.FindFirstByLastName(" ");

            // Assert - [ExpectedException] 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchByAge_AgeMoreThanTwoHundred_ExceptionThrow()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidUserIdGenerator(), new UserValidator());

            // Act
            userStorageService.SearchByAge(253);

            // Assert - [ExpectedException] 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchByAge_AgeIsLessThanOne_ExceptionThrow()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidUserIdGenerator(), new UserValidator());

            // Act
            userStorageService.SearchByAge(0);

            // Assert - [ExpectedException] 
        }

        #endregion

        #endregion
    }
}
