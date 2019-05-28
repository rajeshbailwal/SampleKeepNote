using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NoteService.API.Models;
using NoteService.API.Repository;
using Xunit;

namespace NoteService.Test
{
    public class NoteRepositoryTest:IClassFixture<DatabaseFixture>
    {
        DatabaseFixture fixture;
        private readonly INoteRepository repository;

        public NoteRepositoryTest(DatabaseFixture _fixture)
        {
            this.fixture = _fixture;
            repository = new NoteRepository(fixture.context);
        }

        [Fact]
        public void CreateNoteShouldReturnNoteUser()
        {
            
            var noteUser = new NoteUser {UserId="Mukesh", Notes=this.GetNotes() };

            var actual= repository.CreateNote(noteUser);
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<NoteUser>(actual);

            //List<Note> notes = repository.FindByUserId("Mukesh");
            //var note = notes.Where(n => n.Id == 101).FirstOrDefault();

            //Assert.NotNull(notes);
        }

        [Fact]
        public void DeleteNoteShouldReturnTrue()
        {
            
            string userId = "Mukesh";
            int noteId = 101;
            //List<Note> notes = repository.FindByUserId(userId);
            var actual = repository.DeleteNote(userId, noteId);
            Assert.True(actual);

            List<Note> notes = repository.FindByUserId(userId);
            var note = notes.Where(n => n.Id == noteId).FirstOrDefault();

            Assert.Null(note);
        }

        [Fact]
        public void UpdateNoteShouldReturnNoteUser()
        {
            string userId = "Mukesh";
            int noteId = 101;

            Note note = new Note();
            note.Id = 101;
            note.Title = "IPL 2018";
            note.Content = "Mumbai Indians vs RCB match scheduled  for 4 PM is cancelled";
            note.Category = this.GetCategory();
            note.Reminders = this.GetReminder();
            note.CreatedBy = "Mukesh";
            note.CreationDate = new DateTime();


            var actual = repository.UpdateNote(noteId, userId,note);
            Assert.IsAssignableFrom<NoteUser>(actual);
        }
        
        private Category GetCategory()
        {
            Category category = new Category();
            category.Id = 201;
            category.Name = "Cricket";
            category.Description = "IPL 20-20";
            category.CreatedBy = "Mukesh";
            category.CreationDate = new DateTime();
            return category;
        }

        private List<Reminder> GetReminder()
        {
            List<Reminder> reminders = new List<Reminder>();
            Reminder reminder = new Reminder();
            reminder.Id = 301;
            reminder.Name = "Email-reminder";
            reminder.Description = "sending-mails";
            reminder.Type = "email";
            reminder.CreatedBy = "Mukesh";
            reminder.CreationDate = new DateTime();
            reminders.Add(reminder);
            return reminders;
        }
        private List<Note> GetNotes()
        {
            List<Note> notes = new List<Note>();
            Note note = new Note();
            note.Id = 101;
            note.Title = "IPL 2018";
            note.Content = "Mumbai Indians vs RCB match scheduled  for 4 PM";
            note.Category = this.GetCategory();
            note.Reminders = this.GetReminder();
            note.CreatedBy = "Mukesh";
            note.CreationDate = new DateTime();

            notes.Add(note);
            return notes;
        }
    }
}
