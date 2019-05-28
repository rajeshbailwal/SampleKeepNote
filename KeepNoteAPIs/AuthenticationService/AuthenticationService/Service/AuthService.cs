using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Exceptions;
using AuthenticationService.Models;
using AuthenticationService.Repository;

namespace AuthenticationService.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository repository)
        {
            this._authRepository = repository;
        }
        public bool IsUserExists(string userId)
        {
            return _authRepository.FindUserById(userId) != null ? true : false;
        }

        public User LoginUser(string userId, string password)
        {
            var user = _authRepository.LoginUser(userId, password);
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new UserNotFoundException($"Invalid Username or Password");
            }
        }

        public bool RegisterUser(User user)
        {
            var result = _authRepository.RegisterUser(user);
            if (result)
            {
                return result;
            }
            else
            {
                throw new UserNotCreatedException($"User with this id {user.UserName} already exists");
            }
        }
    }
}
