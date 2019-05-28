using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.API.Exceptions;
using UserService.API.Models;
using UserService.API.Repository;

namespace UserService.API.Service
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public bool DeleteUser(string userId)
        {
            var result = _repository.DeleteUser(userId);

            if (!result)
            {
                throw new UserNotFoundException("This user id does not exist");
            }
            return result;
        }

        public User GetUserById(string userId)
        {
            var result = _repository.GetUserById(userId);

            if (result == null)
            {
                throw new UserNotFoundException("This user id does not exist");
            }
            return result;
        }

        public User RegisterUser(User user)
        {
            var result = _repository.RegisterUser(user);

            if (result == null)
            {
                throw new UserNotCreatedException("This user id already exists");
            }
            return result;
        }

        public bool UpdateUser(string userId, User user)
        {
            var result = _repository.UpdateUser(userId, user);

            if (!result)
            {
                throw new UserNotFoundException("This user id does not exist");
            }
            return result;
        }
    }
}
