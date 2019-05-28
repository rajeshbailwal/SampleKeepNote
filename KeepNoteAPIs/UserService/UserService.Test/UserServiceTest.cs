using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using UserService.API.Exceptions;
using UserService.API.Models;
using UserService.API.Repository;
using Xunit;

namespace UserService.Test
{
    public class UserServiceTest
    {
        #region positive tests

        [Fact]
        public void RegisterUserShouldReturnUser()
        {
            User user = new User { UserId = "Nishant", Name = "Nishant", Password = "admin123", Contact = "9892134560", AddedDate = new DateTime() };
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.RegisterUser(user)).Returns(user);
            var service = new API.Service.UserService(mockRepo.Object);

            var actual = service.RegisterUser(user);

            Assert.NotNull(actual);
            Assert.IsAssignableFrom<User>(actual);
        }

        [Fact]
        public void DeleteUserShouldReturnTrue()
        {
            string userId = "Sachin";
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.DeleteUser(userId)).Returns(true);
            var service = new API.Service.UserService(mockRepo.Object);

            var actual = service.DeleteUser(userId);

            Assert.True(actual);
        }

        [Fact]
        public void UpdateUserShouldreturnTrue()
        {
            string userId = "Sachin";
            User user = new User { UserId = "Mukesh", Name = "Mukesh", Password = "admin123", Contact = "9822445566", AddedDate = new DateTime() };
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.UpdateUser(userId,user)).Returns(true);
            var service = new API.Service.UserService(mockRepo.Object);

            var actual = service.UpdateUser(userId,user);

            Assert.True(actual);
        }

        [Fact]
        public void GetUserByIdShouldReturnUser()
        {
            string userId = "Sachin";
            User user = new User { UserId = "Mukesh", Name = "Mukesh", Password = "admin123", Contact = "9822445566", AddedDate = new DateTime() };
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetUserById(userId)).Returns(user);
            var service = new API.Service.UserService(mockRepo.Object);

            var actual = service.GetUserById(userId);

            Assert.NotNull(actual);
            Assert.IsAssignableFrom<User>(actual);
            Assert.Equal("Mukesh", actual.Name);
        }

        #endregion positive tests

        #region negative tests

        [Fact]
        public void RegisterUserShouldThrowException()
        {
            User user = new User { UserId = "Mukesh", Name = "Mukesh", Password = "admin123", Contact = "9822445566", AddedDate = new DateTime() };
            User _user = null;
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetUserById(user.UserId)).Returns(_user);
            var service = new API.Service.UserService(mockRepo.Object);

            var actual = Assert.Throws<UserNotCreatedException>(() => service.RegisterUser(user));
            Assert.Equal("This user id already exists", actual.Message);

        }

        [Fact]
        public void DeleteUserShouldThrowException()
        {
            string userId = "Sachin";
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.DeleteUser(userId)).Returns(false);
            var service = new API.Service.UserService(mockRepo.Object);

            var actual = Assert.Throws<UserNotFoundException>(() => service.DeleteUser(userId));

            Assert.Equal("This user id does not exist", actual.Message);
        }

        [Fact]
        public void UpdateUserShouldThrowException()
        {
            string userId = "Dinesh";
            User user = new User { UserId = "Dinesh", Name = "Dinesh", Password = "admin123", Contact = "9892134560", AddedDate = new DateTime() };
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.UpdateUser(userId, user)).Returns(false);
            var service = new API.Service.UserService(mockRepo.Object);

            var actual = Assert.Throws<UserNotFoundException>(() => service.UpdateUser(userId, user));

            Assert.Equal("This user id does not exist", actual.Message);
        }

        [Fact]
        public void GetUserByIdShouldThrowException()
        {
            string userId = "Sachin";
            User user = null;
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetUserById(userId)).Returns(user);
            var service = new API.Service.UserService(mockRepo.Object);

            var actual = Assert.Throws<UserNotFoundException>(() => service.GetUserById(userId));

            Assert.Equal("This user id does not exist", actual.Message);
        }


        #endregion negative tests
    }
}
