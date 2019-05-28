using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CategoryService.API.Service;
using CategoryService.API.Models;
using CategoryService.API.Exceptions;
using System.Net;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CategoryService.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService service;
        private string ServerError = "There is something wrong! Please contact admin";

        public CategoryController(ICategoryService _service)
        {
            this.service = _service;
        }
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get(int categoryId)
        {
            try
            {
                return Ok(service.GetCategoryById(categoryId));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ServerError);
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("GetTags/{userId}")]
        public IActionResult Get(string userId)
        {
            try
            {
                return Ok(service.GetAllCategoriesByUserId(userId));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ServerError);
            }
        }

        [HttpGet]
        [Route("GetAllTags")]
        public IActionResult GetAllCategories()
        {
            try
            {
                return Ok(service.GetAllCategories());
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ServerError);
            }
        }

        // POST api/<controller>
        [HttpPost()]
        [Route("AddTag/{userId}")]
        public IActionResult Post(string userId, [FromBody]Category value)
        {
            try
            {
                value.CreationDate = DateTime.Now;
                return Created("", service.CreateCategory(value) );
            }
            catch (CategoryNotCreatedException exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ServerError);
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Category value)
        {
            try
            {
                return Ok(service.UpdateCategory(id, value));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ServerError);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete()]
        [Route("DeleteTag/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(service.DeleteCategory(id));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ServerError);
            }
        }
    }
}
