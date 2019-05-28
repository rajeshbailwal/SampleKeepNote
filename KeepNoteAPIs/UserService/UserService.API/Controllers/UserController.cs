using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.API.Exceptions;
using UserService.API.Models;
using UserService.API.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserService.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService service;
        private string ServerError = "There is something wrong! Please contact admin";

        public UserController(IUserService _service)
        {
            this.service = _service;
        }

        //// GET: api/<controller>
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    try
        //    {
        //        return Ok(service.get());
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError, ServerError);
        //    }
        //}

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                return Ok(service.GetUserById(id));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ServerError);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]User value)
        {
            try
            {
                return Created("", service.RegisterUser(value));
            }
            catch (UserNotFoundException exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ServerError);
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]User value)
        {
            try
            {
                var result = service.UpdateUser(id, value);
                return Ok(value);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ServerError);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                return Ok(service.DeleteUser(id));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ServerError);
            }
        }
    }
}
