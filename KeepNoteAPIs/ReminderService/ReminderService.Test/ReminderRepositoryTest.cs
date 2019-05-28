using System;
using System.Collections.Generic;
using System.Text;
using ReminderService.API.Models;
using ReminderService.API.Repository;
using Xunit;

namespace ReminderService.Test
{
    public class ReminderRepositoryTest:IClassFixture<DatabaseFixture>
    {
        DatabaseFixture fixture;
        private readonly ReminderRepository repository;

        public ReminderRepositoryTest(DatabaseFixture _fixture)
        {
            this.fixture = _fixture;
            repository = new ReminderRepository(fixture.context);
        }

        [Fact]
        public void CreateReminderShouldReturnReminder()
        {
            Reminder reminder = new Reminder { Id = 203, Name = "Books", CreatedBy = "Sachin", Description = "books reminder", CreationDate = new DateTime(), Type = "email" };

            var actual = repository.CreateReminder(reminder);
            Assert.IsAssignableFrom<Reminder>(actual);
        }

        [Fact]
        public void DeleteReminderShouldReturnTrue()
        {
            int Id = 202;

            var actual = repository.DeleteReminder(Id);

            Assert.True(actual);
        }
        [Fact]
        public void GetAllRemindersShouldReturnAList()
        {
            var actual = repository.GetAllReminders();

            Assert.IsAssignableFrom<List<Reminder>>(actual);
            Assert.NotEmpty(actual);
        }

        [Fact]
        public void GetReminderByIdShouldReturnAReminder()
        {
            Reminder reminder = new Reminder { Id = 201, Name = "Sports", CreatedBy = "Mukesh", Description = "sports reminder", CreationDate = new DateTime(), Type = "email" };

            var actual = repository.GetReminderById(201);

            Assert.NotNull(actual);
            Assert.IsAssignableFrom<Reminder>(actual);
        }

        [Fact]
        public void UpdateReminderShouldReturnTrue()
        {
            Reminder reminder = new Reminder { Id = 201, Name = "Sports", CreatedBy = "Mukesh", Description = "sports reminder", CreationDate = new DateTime(), Type = "sms" };

            var actual = repository.UpdateReminder(201, reminder);
            Assert.True(actual);
        }
        private List<Reminder> GetReminders()
        {
            List<Reminder> reminders = new List<Reminder> {
                new Reminder{Id=201, Name="Sports", CreatedBy="Mukesh", Description="sports reminder", CreationDate=new DateTime(), Type="email" },
                 new Reminder{Id=202, Name="Politics", CreatedBy="Mukesh", Description="politics reminder", CreationDate=new DateTime(),Type="email" }
            };

            return reminders;
        }
    }
}
