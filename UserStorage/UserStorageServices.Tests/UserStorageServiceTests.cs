using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserStorageServices.CustomExceptions;
using UserStorageServices.Enums;
using UserStorageServices.Notifications.Concrete;
using UserStorageServices.Services.Abstract;
using UserStorageServices.Services.Concrete;

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
            var userStorageService = new UserStorageServiceMaster();

            // Act
            userStorageService.Add(new User() { Age = 5, LastName = "Petrov", FirstName = "Vasya" });

            // Assert - [ExpectedException]
            Assert.AreEqual(1, userStorageService.Count);
        }

        [TestMethod]
        public void Add_ThreeParamsAllOk()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();

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
            var userStorageService = new UserStorageServiceMaster();

            // Act
            userStorageService.Add(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(FirstNameIsNullOrEmptyException))]
        public void Add_UserFirstNameIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();

            // Act
            userStorageService.Add(new User
            {
                FirstName = null
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(LastNameIsNullOrEmptyException))]
        public void Add_UserLastNameIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();

            // Act
            userStorageService.Add(new User
            {
                FirstName = "Vasya",
                LastName = null,
                Age = 25
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(AgeExceedsLimitsException))]
        public void Add_UserAgeMoreThanTwoHundred_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();

            // Act
            userStorageService.Add(new User
            {
                FirstName = "Kostya",
                LastName = "Petrov",
                Age = 201
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(AgeExceedsLimitsException))]
        public void Add_UserAgeLessThanZero_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();

            // Act
            userStorageService.Add(new User
            {
                FirstName = "Kostya",
                LastName = "Petrov",
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
            var userStorageService = new UserStorageServiceMaster();

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
            var userStorageService = new UserStorageServiceMaster();

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
            var userStorageService = new UserStorageServiceMaster();

            // Act
            userStorageService.Remove(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void Remove_UserNotFound_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();

            // Act
            userStorageService.Remove(new User() { FirstName = "Vasya" });

            // Assert - [ExpectedException]
        }

        #endregion

        #endregion

        #region Search tests

        #region Positive

        #region Returns first user

        #region By concrete condition

        [TestMethod]
        public void FindFirstByFirstName_AllOk()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();

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
            var userStorageService = new UserStorageServiceMaster();

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
            var userStorageService = new UserStorageServiceMaster();

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

        #region Composite 

        [TestMethod]
        public void FindFirst_SearchByFirstNameAndLastName_ReturnUser()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();

            var user = new User()
            {
                Age = 27,
                FirstName = "Petya",
                LastName = "Sidorov"
            };
            userStorageService.Add(user);

            // Act
            var resUser = userStorageService.FindFirst(u => u.FirstName == "Petya" && u.LastName == "Sidorov");

            // Assert 
            Assert.AreEqual(user, resUser);
        }

        [TestMethod]
        public void FindFirst_SearchByFirstNameAndAge_ReturnUser()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();

            var user = new User()
            {
                Age = 27,
                FirstName = "Petya",
                LastName = "Sidorov"
            };
            userStorageService.Add(user);

            // Act
            var resUser = userStorageService.FindFirst(u => u.FirstName == "Petya" && u.Age == 27);

            // Assert 
            Assert.AreEqual(user, resUser);
        }

        [TestMethod]
        public void FindFirst_SearchByLastNameAndAge_ReturnUser()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();

            var user = new User()
            {
                Age = 27,
                FirstName = "Petya",
                LastName = "Sidorov"
            };
            userStorageService.Add(user);

            // Act
            var resUser = userStorageService.FindFirst(u => u.Age == 27 && u.LastName == "Sidorov");

            // Assert 
            Assert.AreEqual(user, resUser);
        }

        [TestMethod]
        public void FindFirst_SearchByFirstNameAndLastNameAndAge_ReturnUser()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();

            var user = new User()
            {
                Age = 27,
                FirstName = "Petya",
                LastName = "Sidorov"
            };
            userStorageService.Add(user);

            // Act
            var resUser =
                userStorageService.FindFirst(u => u.FirstName == "Petya" && u.LastName == "Sidorov" && u.Age == 27);

            // Assert 
            Assert.AreEqual(user, resUser);
        }

        #endregion

        #endregion

        #region Returns IEnumerable<User>

        [TestMethod]
        public void SearchByFirstName_AllOk()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();
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
            var userStorageService = new UserStorageServiceMaster();
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
            var userStorageService = new UserStorageServiceMaster();
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
            var userStorageService = new UserStorageServiceMaster();

            // Act
            userStorageService.FindFirstByFirstName(string.Empty);

            // Assert - [ExpectedException] 
        }

        [TestMethod]
        [ExpectedException(typeof(LastNameIsNullOrEmptyException))]
        public void FindFirstByLastName_WhiteSpace_ExceptionThrow()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();

            // Act
            userStorageService.FindFirstByLastName(" ");

            // Assert - [ExpectedException] 
        }

        [TestMethod]
        [ExpectedException(typeof(AgeExceedsLimitsException))]
        public void SearchByAge_AgeMoreThanTwoHundred_ExceptionThrow()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();

            // Act
            userStorageService.SearchByAge(253);

            // Assert - [ExpectedException] 
        }

        [TestMethod]
        [ExpectedException(typeof(AgeExceedsLimitsException))]
        public void SearchByAge_AgeIsLessThanOne_ExceptionThrow()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();

            // Act
            userStorageService.SearchByAge(0);

            // Assert - [ExpectedException] 
        }

        #endregion

        #endregion

        #region Master and slave modes

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Add_Slave_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageServiceSlave();

            // Act
            userStorageService.Add(new User
            {
                FirstName = "Vasya",
                LastName = "Petrov",
                Age = 25
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Remove_Slave_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageServiceSlave();

            // Act
            userStorageService.Remove(new User
            {
                FirstName = "Vasya",
                LastName = "Petrov",
                Age = 20
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Add_Master_AllAdded()
        {
            // Arrange
            var person = new User
            {
                FirstName = "Justin",
                LastName = "Petrov",
                Age = 21
            };

            var slave1 = new UserStorageServiceLog(new UserStorageServiceSlave());
            var slave2 = new UserStorageServiceSlave();

            var master = new UserStorageServiceLog(
                new UserStorageServiceMaster(slaveServices: new IUserStorageService[] { slave1, slave2 }));

            // Act
            master.Add(person);

            // Assert
            Assert.IsTrue(master.Count == 1 && slave1.Count == 1 && slave2.Count == 1);
        }

        [TestMethod]
        public void Remove_Master_AllRemoved()
        {
            // Arrange
            var person = new User
            {
                FirstName = "Justin",
                LastName = "Petrov",
                Age = 21
            };

            var slave1 = new UserStorageServiceLog(new UserStorageServiceSlave());
            var slave2 = new UserStorageServiceSlave();

            var master = new UserStorageServiceLog(
                new UserStorageServiceMaster(slaveServices: new IUserStorageService[] { slave1, slave2 }));

            // Act
            master.Add(person);

            master.Remove(person);

            // Assert
            Assert.IsTrue(master.Count == 0 && slave1.Count == 0 && slave2.Count == 0);
        }

        #endregion

        #region NotificationTests

        [TestMethod]
        public void Add_WithNotification_AllAdded()
        {
            // Arrange
            var vasya = new User
            {
                FirstName = "Vasya",
                LastName = "Petrov",
                Age = 20
            };

            var slave = new UserStorageServiceSlave();
            var master = new UserStorageServiceMaster();
            ((NotificationSender)master.Sender).Receiver = slave.Receiver;


            // Act
            master.Add(vasya);

            // Assert
            Assert.IsTrue(master.Count == 1 && slave.Count == 1);
        }

        [TestMethod]
        public void Remove_WithNotification_AllRemoved()
        {
            // Arrange
            var vasya = new User
            {
                FirstName = "Vasya",
                LastName = "Petrov",
                Age = 20
            };

            var slave = new UserStorageServiceSlave();
            var master = new UserStorageServiceMaster();
            ((NotificationSender)master.Sender).Receiver = slave.Receiver;
            master.Add(vasya);

            // Act
            master.Remove(vasya.Id);

            // Assert
            Assert.IsTrue(master.Count == 0 && slave.Count == 0);
        }

        #endregion
    }
}
