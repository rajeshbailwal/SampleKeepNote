using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoteService.API.Models;

namespace NoteService.API.Service
{
    public interface INoteService
    {
        NoteUser CreateNote(NoteUser noteUser);
        NoteUser AddNote(string userId, Note note);
        bool DeleteNote(string userId, int noteId);
        NoteUser UpdateNote(int noteId, string userId, Note note);
        List<Note> GetAllNotes(string userId);
        Note GetNote(string userId, int noteId);

    }
}
