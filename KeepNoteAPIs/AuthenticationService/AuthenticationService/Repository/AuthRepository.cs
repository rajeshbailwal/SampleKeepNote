using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Models;

namespace AuthenticationService.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IAuthenticationContext _authenticationContext;
        public AuthRepository(IAuthenticationContext authenticationContext)
        {
            _authenticationContext = authenticationContext;
        }
        public User FindUserById(string userId)
        {
            return _authenticationContext.Users.Find(userId.ToLower());
        }

        public User LoginUser(string userId, string password)
        {
            var _user = _authenticationContext.Users.FirstOrDefault(usr => usr.UserName.ToLower() == userId.ToLower() && usr.Password == password);
            return _user;
        }

        public bool RegisterUser(User user)
        {
            user.UserName = user.UserName.ToLower();

            _authenticationContext.Users.Add(user);
            return _authenticationContext.SaveChanges() > 0 ? true : false;
        }
    }
}
