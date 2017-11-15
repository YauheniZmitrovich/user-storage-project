using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserStorageServices.Concrete.Repositories;
using UserStorageServices.Concrete.SerializationStrategies;

namespace UserStorageServices.Tests
{
    [TestClass]
    public class UserFileRepositoryTests
    {
        [TestMethod]
        public void StartStop_BinarySerializer_Success()
        {
            // Arrange
            var stateIn = new UserFileRepository();
            var stateOut = new UserFileRepository();

            var users = new HashSet<User>
            {
                new User { Age = 15, FirstName = "Vasya", LastName = "Petrov" },
                new User { Age = 20, FirstName = "Petr", LastName = "Vasin" }
            };

            foreach (var user in users)
            {
                stateIn.Set(user);
            }

            // Act
            stateIn.Stop();
            stateOut.Start();

            // Assert
            var resUsers = stateOut.Query(u => true).ToList();

            CollectionAssert.AreEqual(resUsers, users.ToList());
        }

        [TestMethod]
        public void StartAndStop_XmlSerializer_Success()
        {
            // Arrange
            var stateIn = new UserFileRepository(serializationStrategy: new XmlUserSerializationStrategy());
            var stateOut = new UserFileRepository(serializationStrategy: new XmlUserSerializationStrategy());

            var users = new HashSet<User>
            {
                new User { Age = 15, FirstName = "Vasya", LastName = "Petrov" },
                new User { Age = 20, FirstName = "Petr", LastName = "Vasin" }
            };

            foreach (var user in users)
            {
                stateIn.Set(user);
            }

            // Act
            stateIn.Stop();
            stateOut.Start();

            // Assert
            var resUsers = stateOut.Query(u => true).ToList();
            CollectionAssert.AreEqual(resUsers, users.ToList());
        }
    }
}
