using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using NoteService.API.Models;
using NoteService.API.Repository;

namespace NoteService.Test
{
    public class NoteServiceTest
    {
        [Fact]
        public void CreateNoteShouldReturnNoteUser()
        {
            var note = new NoteUser {UserId="Mukesh", Notes=this.GetNotes() };
            var mockRepo = new Mock<INoteRepository>();
            mockRepo.Setup(repo => repo.CreateNote(note)).Returns(note);
            var service = new API.Service.NoteService(mockRepo.Object);

            var actual = service.CreateNote(note);
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<NoteUser>(actual);
        }

        [Fact]
        public void AddNoteShouldReturnNoteUser()
        {
            var note = this.GetNote();
            var noteUser = new NoteUser {UserId="Mukesh", Notes=this.GetNotes() };
            int noteId = 101;
            string userId = "Mukesh";
            var mockRepo = new Mock<INoteRepository>();
            mockRepo.Setup(repo => repo.UpdateNote(noteId, userId,note)).Returns(noteUser);
            var service = new API.Service.NoteService(mockRepo.Object);

            var actual = service.AddNote(userId,note);
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<NoteUser>(actual);
        }

        [Fact]
        public void UpdateNoteShouldReturnNoteUser()
        {
            //var note = this.GetNote();
            var noteUser = new NoteUser { UserId = "Mukesh", Notes = this.GetNotes() };
            int noteId = 101;
            string userId = "Mukesh";

            Note note = new Note();
            note.Id = 101;
            note.Title = "IPL 2018";
            note.Content = "Mumbai Indians vs RCB match scheduled  for 4 PM is cancelled";
            note.Category = this.GetCategory();
            note.Reminders = this.GetReminder();
            note.CreatedBy = "Mukesh";
            note.CreationDate = new DateTime();

            var mockRepo = new Mock<INoteRepository>();
            mockRepo.Setup(repo => repo.UpdateNote(noteId, userId, note)).Returns(noteUser);
            var service = new API.Service.NoteService(mockRepo.Object);

            var actual = service.UpdateNote(noteId,userId, note);
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<NoteUser>(actual);
        }

        [Fact]
        public void DeleteNoteShouldReturnTrue()
        {
            int noteId = 101;
            string userId = "Mukesh";
            var mockRepo = new Mock<INoteRepository>();
            mockRepo.Setup(repo => repo.DeleteNote(userId, noteId)).Returns(true);
            var service = new API.Service.NoteService(mockRepo.Object);

            var actual = service.DeleteNote(userId, noteId);

            Assert.True(actual);
        }

        [Fact]
        public void GetAllNotesShouldReturnAList()
        {
            string userId = "Mukesh";
            var mockRepo = new Mock<INoteRepository>();
            mockRepo.Setup(repo => repo.FindByUserId(userId)).Returns(this.GetNotes());
            var service = new API.Service.NoteService(mockRepo.Object);

            var actual = service.GetAllNotes(userId);
            Assert.NotEmpty(actual);
            Assert.IsAssignableFrom<List<Note>>(actual);
        }

        [Fact]
        public void GetNoteByNoteIdShouldReturnNote()
        {
            int noteId = 101;
            string userId = "Mukesh";
            var mockRepo = new Mock<INoteRepository>();
            mockRepo.Setup(repo => repo.FindByUserId(userId)).Returns(this.GetNotes());
            var service = new API.Service.NoteService(mockRepo.Object);

            var actual = service.GetNote(userId, noteId); 
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

        private Note GetNote()
        {
            Note note = new Note();
            note.Id = 101;
            note.Title = "IPL 2018";
            note.Content = "Mumbai Indians vs RCB match scheduled  for 4 PM";
            note.Category = this.GetCategory();
            note.Reminders = this.GetReminder();
            note.CreatedBy = "Mukesh";
            note.CreationDate = new DateTime();

            return note;
        }
        private List<Note> GetNotes()
        {
            List<Note> notes = new List<Note>();
            notes.Add(this.GetNote());
            return notes;
        }
    }
}
