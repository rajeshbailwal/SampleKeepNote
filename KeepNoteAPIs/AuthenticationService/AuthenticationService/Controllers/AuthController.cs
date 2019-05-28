using AuthenticationService.Exceptions;
using AuthenticationService.Models;
using AuthenticationService.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AuthenticationService.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _service;
        private readonly ITokenService _tokenService;
        private readonly string Internal_error = "Something went wrong, please contact admin!";

        //public AuthController(IAuthService service)
        //{
        //    this._service = service;
        //}

        public AuthController(IAuthService service, ITokenService tokenService)
        {
            this._service = service;
            this._tokenService = tokenService;
        }

        // POST api/<controller>
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody]User user)
        {
            try
            {
                if (_service.IsUserExists(user.UserName))
                {
                    return StatusCode((int)HttpStatusCode.Conflict, $"A User Alreay Exists with the same User Id : {user.UserName}");
                }
                else
                {
                    _service.RegisterUser(user);

                    return new CreatedResult("", true);
                }
            }
            catch (UserNotCreatedException exception)
            {
                //return StatusCode((int)HttpStatusCode.Conflict, exception.Message);
                return new ConflictResult();
            }
            catch (System.Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, this.Internal_error);
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]User user)
        {
            try
            {
                User usr = _service.LoginUser(user.UserName, user.Password);

                string tokenValue = _tokenService.GetJWTToken(user.UserName);
                return Ok(tokenValue);
                //return Ok(usr);
            }
            catch (UserNotFoundException exception)
            {
                return StatusCode((int)HttpStatusCode.NotFound, exception.Message);
            }
            catch (System.Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, this.Internal_error);
            }
        }
    }
}
