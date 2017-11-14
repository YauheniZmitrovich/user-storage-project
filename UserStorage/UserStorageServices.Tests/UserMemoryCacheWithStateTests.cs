using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserStorageServices.Concrete.Repositories;

namespace UserStorageServices.Tests
{
    [TestClass]
    public class UserMemoryCacheWithStateTests
    {
        [TestMethod]
        public void StartStop_NoArguments_Success()
        {
            // Arrange
            var stateIn = new UserMemoryCacheWithState();
            var stateOut = new UserMemoryCacheWithState();

            var users = new HashSet<User>
            {
                new User {Age = 15, FirstName = "Vasya", LastName = "Petrov", Id = Guid.NewGuid()},
                new User {Age = 20, FirstName = "Petr", LastName = "Vasin", Id = Guid.NewGuid()}
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

            CollectionAssert.AreEqual( resUsers,users.ToList());
        }
    }
}
