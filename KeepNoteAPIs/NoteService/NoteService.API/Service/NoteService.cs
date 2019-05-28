using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoteService.API.Exceptions;
using NoteService.API.Models;
using NoteService.API.Repository;

namespace NoteService.API.Service
{
    public class NoteService : INoteService
    {
        private INoteRepository _notesRepository;
        public NoteService(INoteRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        public NoteUser AddNote(string userId, Note note)
        {
            var result = _notesRepository.UpdateNote(note.Id, userId, note);

            if(result==null)
            {
                throw new Exception("Note not added.");
            }
            return result;
        }

        public NoteUser CreateNote(NoteUser noteUser)
        {
            var result = _notesRepository.CreateNote(noteUser);

            if (result == null)
            {
                throw new NoteNotFoundExeption("This category id already exists");
            }
            return result;
        }

        public bool DeleteNote(string userId, int noteId)
        {
            return _notesRepository.DeleteNote(userId, noteId);
        }

        public List<Note> GetAllNotes(string userId)
        {
            return _notesRepository.FindByUserId(userId);
        }

        public Note GetNote(string userId, int noteId)
        {
            return _notesRepository.GetNoteById(userId, noteId);
        }

        public NoteUser UpdateNote(int noteId, string userId, Note note)
        {
            return _notesRepository.UpdateNote(noteId,userId, note);
        }
    }
}
