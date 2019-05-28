using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReminderService.API.Models;

namespace ReminderService.API.Service
{
    public interface IReminderService
    {
        Reminder CreateReminder(Reminder reminder);
        bool DeleteReminder(int reminderId);
        bool UpdateReminder(int reminderId, Reminder reminder);
        Reminder GetReminderById(int reminderId);
        List<Reminder> GetAllReminders();
        List<Reminder> GetAllRemindersByUserId(string userId);
    }
}
