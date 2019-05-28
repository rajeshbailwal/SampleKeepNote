using System;
using Xunit;
using Moq;
using AuthenticationService.Service;
using AuthenticationService.Models;
using AuthenticationService.Controllers;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Exceptions;

namespace AuthenticationService.Test
{
    public class AuthControllerTest
    {
        [Fact]
        public void RegisterShouldReturnCreatedResult()
        {
            var mockService = new Mock<IAuthService>();
            var mockTokenService = new Mock<ITokenService>();
            User user = new User { UserName = "sachin", Password = "admin123", FirstName = "Sachin", LastName = "Gupta", Role = "admin", AddedDate = new DateTime() };


            mockService.Setup(service => service.RegisterUser(user)).Returns(true);
            var controller = new AuthController(mockService.Object, mockTokenService.Object);

            var actual = controller.Register(user);

            var actionReult = Assert.IsType<CreatedResult>(actual);
            Assert.True((bool)actionReult.Value);
        }

        [Fact]
        public void LoginShouldReturnOkResult()
        {
            var mockService = new Mock<IAuthService>();
            var mockTokenService = new Mock<ITokenService>();

            var loginuser = new User { UserName = "sachin", Password = "admin123" };
            User user = new User { UserName = "sachin", Password = "admin123", FirstName = "Sachin", LastName = "Gupta", Role = "admin", AddedDate = new DateTime() };

            mockService.Setup(service => service.LoginUser(loginuser.UserName, loginuser.Password)).Returns(user);
            var controller = new AuthController(mockService.Object, mockTokenService.Object);

            var actual = controller.Login(loginuser);

            Assert.IsType<OkObjectResult>(actual);
            //Assert.IsAssignableFrom<User>(actionReult.Value);
        }



        [Fact]
        public void RegisterShouldReturnConflictResult()
        {
            var mockService = new Mock<IAuthService>();
            var mockTokenService = new Mock<ITokenService>();

            User user = new User { UserName = "sachin", Password = "admin123", FirstName = "Sachin", LastName = "Gupta", Role = "admin", AddedDate = new DateTime() };


            mockService.Setup(service => service.RegisterUser(user)).Throws<UserNotCreatedException>();
            var controller = new AuthController(mockService.Object, mockTokenService.Object);

            var actual = controller.Register(user);

            var actionResult = Assert.IsType<ConflictResult>(actual);
        }

        [Fact]
        public void LoginShouldReturnNotFoundResult()
        {
            var mockService = new Mock<IAuthService>();
            var mockTokenService = new Mock<ITokenService>();

            var loginuser = new User { UserName = "sachin", Password = "admin123" };
            User user = new User { UserName = "sachin", Password = "admin123", FirstName = "Sachin", LastName = "Gupta", Role = "admin", AddedDate = new DateTime() };

            mockService.Setup(service => service.LoginUser(loginuser.UserName, loginuser.Password)).Throws<UserNotFoundException>();
            var controller = new AuthController(mockService.Object, mockTokenService.Object);

            var actual = controller.Login(loginuser);

            var actionReult = Assert.IsType<NotFoundResult>(actual);
        }
    }
}
