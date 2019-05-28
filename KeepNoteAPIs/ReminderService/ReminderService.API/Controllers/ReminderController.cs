using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReminderService.API.Exceptions;
using ReminderService.API.Models;
using ReminderService.API.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReminderService.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ReminderController : Controller
    {
        private readonly IReminderService service;
        private string ServerError = "There is something wrong! Please contact admin";

        public ReminderController(IReminderService _service)
        {
            this.service = _service;
        }

        // GET: api/<controller>
        [HttpGet]
        [Route("GetReminders")]
        public IActionResult Get()
        {
            try
            {
                return Ok(service.GetAllReminders());
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ServerError);
            }
        }

        [HttpGet]
        [Route("GetReminders/{userId}")]
        public IActionResult GetRemindersByUser(string userId)
        {
            try
            {
                return Ok(service.GetAllRemindersByUserId(userId));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ServerError);
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(service.GetReminderById(id));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ServerError);
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Route("AddReminder/{userId}")]
        public IActionResult Post(string userId, [FromBody]Reminder value)
        {
            try
            {
                value.CreationDate = DateTime.Now;
                value.CreatedBy = userId;
                return Created("", service.CreateReminder(value));
            }
            catch (ReminderNotFoundException exception)
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
        public IActionResult Put(int id, [FromBody]Reminder value)
        {
            try
            {
                var result = service.UpdateReminder(id, value);
                return Ok(value);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ServerError);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("DeleteReminder/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(service.DeleteReminder(id));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ServerError);
            }

        }
    }
}
