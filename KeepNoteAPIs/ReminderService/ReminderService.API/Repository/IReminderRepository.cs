using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReminderService.API.Models;

namespace ReminderService.API.Repository
{
    public interface IReminderRepository
    {
        Reminder CreateReminder(Reminder reminder);
        bool DeleteReminder(int reminderId);
        bool UpdateReminder(int reminderId, Reminder reminder);
        Reminder GetReminderById(int reminderId);
        List<Reminder> GetAllReminders();
        List<Reminder> GetAllRemindersByUserId(string userId);
        Reminder GetReminderByName(string name);
    }
}
