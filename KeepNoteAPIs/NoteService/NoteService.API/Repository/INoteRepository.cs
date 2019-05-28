using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoteService.API.Models;

namespace NoteService.API.Repository
{
    public interface INoteRepository
    {
        NoteUser CreateNote(NoteUser noteUser);
        bool DeleteNote(string userId, int noteId);
        NoteUser UpdateNote(int noteId, string userId, Note note);
        List<Note> FindByUserId(string userId);
        Note GetNoteById(string userId, int noteId);
    }
}
