using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using ReminderService.API.Models;

namespace ReminderService.API.Repository
{
    public class ReminderRepository : IReminderRepository
    {
        private IReminderContext _dbContext;
        public ReminderRepository(IReminderContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Reminder CreateReminder(Reminder reminder)
        {
            reminder.Id= FindMaxId() + 1;

            _dbContext.Reminders.InsertOne(reminder);

            return _dbContext.Reminders.Find(x => x.Id == reminder.Id).FirstOrDefault();
        }

        public bool DeleteReminder(int reminderId)
        {
            DeleteResult actionResult
               = _dbContext.Reminders.DeleteOne(x => x.Id == reminderId);

            return actionResult.IsAcknowledged
                && actionResult.DeletedCount > 0;
        }

        public List<Reminder> GetAllReminders()
        {
            return _dbContext.Reminders.Find(_ => true).ToList();
        }

        public List<Reminder> GetAllRemindersByUserId(string userId)
        {
            return _dbContext.Reminders.Find(x => x.CreatedBy.ToLower() == userId.ToLower()).ToList();
        }

        public Reminder GetReminderById(int reminderId)
        {
            return _dbContext.Reminders.Find(x => x.Id == reminderId).FirstOrDefault();
        }

        public Reminder GetReminderByName(string name)
        {
            return _dbContext.Reminders.Find(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
        }

        public bool UpdateReminder(int reminderId, Reminder reminder)
        {
            var item = GetReminderById(reminderId) ?? new Reminder();
            item = reminder;

            ReplaceOneResult actionResult = _dbContext.Reminders.ReplaceOne(x => x.Id.Equals(reminderId),
                item, new UpdateOptions { IsUpsert = true });

            return actionResult.IsAcknowledged
                && actionResult.ModifiedCount > 0;
        }

        private int FindMaxId()
        {
            var records = _dbContext.Reminders.AsQueryable().FirstOrDefault();

            if (records == null)
            {
                return 0;
            }

            return _dbContext.Reminders.AsQueryable().Max(x => x.Id);
        }
    }
}
