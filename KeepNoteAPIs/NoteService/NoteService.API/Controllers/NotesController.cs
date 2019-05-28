using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteService.API.Models;
using NoteService.API.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NoteService.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly INoteService service;
        private string ServerError = "There is something wrong! Please contact admin";

        public NotesController(INoteService _service)
        {
            this.service = _service;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("GetNotes/{userId}")]
        public IActionResult Get(string userId)
        {
            try
            {
                return Ok(service.GetAllNotes(userId));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ServerError);
            }
        }

        // POST api/<controller>
        [HttpPost()]
        [Route("AddNote/{userId}")]
        public IActionResult Post(string userId,[FromBody]Note note)
        {
            try
            {
                if (userId != null && userId.Length > 0 && note != null)
                {
                    NoteUser noteUser = new NoteUser();
                    noteUser.UserId = userId;
                    noteUser.Notes = new List<Note>();
                    noteUser.Notes.Add(note);

                    return Ok(service.CreateNote(noteUser).Notes);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ServerError);
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}/{userId}")]
        public IActionResult Put(int id,string userId, [FromBody]Note value)
        {
            try
            {

                return Ok(service.UpdateNote(id,userId, value).Notes.FirstOrDefault());
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ServerError);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{userId}/{id}")]
        public IActionResult Delete(string userId,int id)
        {
            try
            {
                return Ok(service.DeleteNote(userId,id));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ServerError);
            }
        }

        [HttpGet]
        [Route("GetNotesById/{userId}/{noteId}")]
        public IActionResult GetNote(string userId,int noteId)
        {
            try
            {
                return Ok(service.GetNote(userId, noteId));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ServerError);
            }
        }
    }
}
