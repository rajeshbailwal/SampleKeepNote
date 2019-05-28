using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Models;

namespace AuthenticationService.Service
{
    public interface IAuthService
    {
        bool IsUserExists(string userId);
        bool RegisterUser(User user);
        User LoginUser(string userId, string password);
    }
}
