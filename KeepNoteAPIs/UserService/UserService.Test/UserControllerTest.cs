using System;
using System.Collections.Generic;
using System.Text;
using UserService.API.Models;
using Xunit;
using Moq;
using UserService.API.Service;
using UserService.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Test
{
    public class UserControllerTest
    {
        [Fact]
        public void GetByUserIdShouldReturnOk()
        {
            string userId = "Mukesh";
            User user = new User { UserId = "Mukesh", Name = "Mukesh", Password = "admin123", Contact = "9812345670", AddedDate = new DateTime() };
            var mockService = new Mock<IUserService>();
            mockService.Setup(service => service.GetUserById(userId)).Returns(user);
            var controller = new UserController(mockService.Object);

            var actual = controller.Get(userId);

            var actionReult = Assert.IsType<OkObjectResult>(actual);
            Assert.IsAssignableFrom<User>(actionReult.Value);
        }

        [Fact]
        public void DeleteShouldReturnOK()
        {
            var mockService = new Mock<IUserService>();
            mockService.Setup(service => service.DeleteUser("Sachin")).Returns(true);
            var controller = new UserController(mockService.Object);

            var actual = controller.Delete("Sachin");

            var actionReult = Assert.IsType<OkObjectResult>(actual);
            var actualValue = actionReult.Value;
            var expected = true;
            Assert.Equal(expected, actualValue);
        }

        [Fact]
        public void PutShouldReturnUser()
        {
            var mockService = new Mock<IUserService>();
            User user = new User { UserId = "Mukesh", Name = "Mukesh", Password = "admin121", Contact = "9812345670", AddedDate = new DateTime() };
            mockService.Setup(service => service.UpdateUser("Mukesh",user)).Returns(true);
            var controller = new UserController(mockService.Object);

            var actual = controller.Put("Mukesh",user);

            var actionReult = Assert.IsType<OkObjectResult>(actual);
            var actualValue = actionReult.Value;
            Assert.IsAssignableFrom<User>(actualValue);
        }


        [Fact]
        public void POSTShouldReturnCreated()
        {
            var mockService = new Mock<IUserService>();
            User user = new User
            {
                UserId = "Sachin",
                Name = "Sachin",
                Password = "admin123",
                Contact = "8987653412",
                AddedDate = new DateTime()
            };
 
            mockService.Setup(service => service.RegisterUser(user)).Returns(user);
            var controller = new UserController(mockService.Object);

            var actual = controller.Post(user);

            var actionReult = Assert.IsType<CreatedResult>(actual);
            var actualValue = actionReult.Value;
            Assert.IsAssignableFrom<User>(actualValue);
        }


        private List<User> GetUsers()
        {
            List<User> users = new List<User> {new User{ UserId="Mukesh", Name="Mukesh", Password="admin123", Contact="9812345670", AddedDate=new DateTime()},
                new User{ UserId="Sachin", Name="Sachin", Password="admin123", Contact="8987653412", AddedDate=new DateTime()} };

            return users;
        }
    }
}
