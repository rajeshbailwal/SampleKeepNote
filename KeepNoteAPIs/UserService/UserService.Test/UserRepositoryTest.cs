using System;
using System.Collections.Generic;
using System.Text;
using UserService.API.Models;
using UserService.API.Repository;
using Xunit;

namespace UserService.Test
{
    public class UserRepositoryTest:IClassFixture<DatabaseFixture>
    {
        DatabaseFixture fixture;
        private readonly IUserRepository repository;

        public UserRepositoryTest(DatabaseFixture _fixture)
        {
            repository = new UserRepository(_fixture.context);
        }

        [Fact]
        public void RegisterUserShouldReturnUser()
        {
            User user = new User {UserId="Nishant", Name="Nishant", Password="admin123", Contact="9892134560", AddedDate=new DateTime() };

            var actual = repository.RegisterUser(user);

            Assert.NotNull(actual);
            Assert.IsAssignableFrom<User>(actual);
        }

        [Fact]
        public void DeleteUserShouldReturnTrue()
        {
            string userId = "Sachin";

            var actual = repository.DeleteUser(userId);

            Assert.True(actual);
        }

        [Fact]

        public void UpdateUserShouldReturnTrue()
        {
            User user = new User { UserId = "Mukesh", Name = "Mukesh", Password = "admin123", Contact = "9822445566", AddedDate = new DateTime() };

            var actual = repository.UpdateUser("Mukesh", user);

            Assert.True(actual);
        }

        [Fact]
        public void GetUserByIdShouldReturnUser()
        {
            string userId = "Mukesh";

            var actual = repository.GetUserById(userId);

            Assert.NotNull(actual);
            Assert.IsAssignableFrom<User>(actual);
        }
    }
}
