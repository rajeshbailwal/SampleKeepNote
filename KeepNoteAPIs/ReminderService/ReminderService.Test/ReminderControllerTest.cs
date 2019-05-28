using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using ReminderService.API.Models;
using ReminderService.API.Service;
using ReminderService.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ReminderService.Test
{
    public class ReminderControllerTest
    {
        [Fact]
        public void GetByCategoryIdShouldReturnOk()
        {
            int reminderId = 101;
            Reminder reminder= new Reminder { Id = 201, Name = "Sports", CreatedBy = "Mukesh", Description = "sports reminder", CreationDate = new DateTime(), Type = "email" };
            var mockService = new Mock<IReminderService>();
            mockService.Setup(service => service.GetReminderById(reminderId)).Returns(reminder);
            var controller = new ReminderController(mockService.Object);

            var actual = controller.Get(reminderId);

            var actionReult = Assert.IsType<OkObjectResult>(actual);
            Assert.IsAssignableFrom<Reminder>(actionReult.Value);
        }

        [Fact]
        public void GetShouldReturnAList()
        {
            var mockService = new Mock<IReminderService>();
            mockService.Setup(service => service.GetAllReminders()).Returns(this.GetReminders());
            var controller = new ReminderController(mockService.Object);

            var actual = controller.Get();

            var actionReult = Assert.IsType<OkObjectResult>(actual);
            Assert.IsAssignableFrom<List<Reminder>>(actionReult.Value);
        }

        [Fact]
        public void DeleteShouldReturnOK()
        {
            var mockService = new Mock<IReminderService>();
            //Note note = new Note { Title = "Sample", NoteId = 1 };
            mockService.Setup(service => service.DeleteReminder(201)).Returns(true);
            var controller = new ReminderController(mockService.Object);

            var actual = controller.Delete(201);

            var actionReult = Assert.IsType<OkObjectResult>(actual);
            var actualValue = actionReult.Value;
            var expected = true;
            Assert.Equal(expected, actualValue);
        }

        [Fact]
        public void POSTShouldReturnCreated()
        {
            var mockService = new Mock<IReminderService>();
            Reminder reminder = new Reminder { Id = 201, Name = "Sports", CreatedBy = "Mukesh", Description = "sports reminder", CreationDate = new DateTime(), Type = "email" };

            mockService.Setup(service => service.CreateReminder(reminder)).Returns(reminder);
            var controller = new ReminderController(mockService.Object);

            var actual = controller.Post(reminder.CreatedBy,reminder);

            var actionReult = Assert.IsType<CreatedResult>(actual);
            var actualValue = actionReult.Value;
            Assert.IsAssignableFrom<Reminder>(actualValue);
        }
        private List<Reminder> GetReminders()
        {
            List<Reminder> reminders =new List<Reminder>{ new Reminder { Id = 201, Name = "Sports", CreatedBy = "Mukesh", Description = "sports reminder", CreationDate = new DateTime(), Type = "email" },
                 new Reminder { Id = 202, Name = "Politics", CreatedBy = "Mukesh", Description = "politics reminder", CreationDate = new DateTime(), Type = "email" }};

            return reminders;
        }
    }
}
