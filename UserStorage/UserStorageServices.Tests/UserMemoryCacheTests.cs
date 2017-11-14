﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserStorageServices.Concrete.Repositories;

namespace UserStorageServices.Tests
{
    [TestClass]
    public class UserMemoryCacheTests
    {
        [TestMethod]
        public void Get_User_Found()
        {
            // Arrange
            var userMemoryCache = new UserMemoryCache();
            User user = (new User()
            {
                Id = Guid.NewGuid(),
                Age = 20,
                FirstName = "Vasya",
                LastName = "Petrov"
            });
            userMemoryCache.Set(user);

            // Act
            var result = userMemoryCache.Get(user.Id);

            // Assert
            Assert.AreEqual(result, user);
        }

        [TestMethod]
        public void Set_User_Seted()
        {
            // Arrange
            var userMemoryCache = new UserMemoryCache();

            // Act
            userMemoryCache.Set(new User()
            {
                Id = Guid.NewGuid(),
                Age = 20,
                FirstName = "Vasya",
                LastName = "Petrov"
            });

            // Assert
            Assert.AreEqual(userMemoryCache.Count, 1);
        }

        [TestMethod]
        public void Delete_User_Deleted()
        {
            // Arrange
            var userMemoryCache = new UserMemoryCache();
            var user = new User()
            {
                Age = 20,
                FirstName = "Vasya",
                LastName = "Petrov"
            };
            userMemoryCache.Set(user);

            // Act
            userMemoryCache.Delete(user.Id);

            // Assert
            Assert.AreEqual(userMemoryCache.Count, 0);
        }

        [TestMethod]
        public void Query_User_Found()
        {
            // Arrange
            var userMemoryCache = new UserMemoryCache();
            userMemoryCache.Set(new User()
            {
                Id = Guid.NewGuid(),
                Age = 20,
                FirstName = "Vasya",
                LastName = "Petrov"
            });

            // Act
            var result = userMemoryCache.Query(u => u.Age == 20);

            // Assert
            Assert.AreEqual("Vasya",result.FirstOrDefault()?.FirstName);
        }
    }
}
