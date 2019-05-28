using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReminderService.API.Exceptions;
using ReminderService.API.Models;
using ReminderService.API.Repository;

namespace ReminderService.API.Service
{
    public class ReminderService:IReminderService
    {
        private IReminderRepository _reminderRepository;

        public ReminderService(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        public Reminder CreateReminder(Reminder reminder)
        {
            //check if reminder is already exist
            var result = _reminderRepository.GetReminderByName(reminder.Name);

            if (result != null)
            {
                throw new ReminderNotCreatedException("Reminder with same name is already exists");
            }

            result = _reminderRepository.CreateReminder(reminder);

            if (result == null)
            {
                throw new ReminderNotCreatedException("This reminder id already exists");
            }
            return result;
        }

        public bool DeleteReminder(int reminderId)
        {
            var result = _reminderRepository.DeleteReminder(reminderId);

            if (!result)
            {
                throw new ReminderNotFoundException("This reminder id not found");
            }
            return result;
        }

        public List<Reminder> GetAllReminders()
        {
            return _reminderRepository.GetAllReminders();
        }
         
        public List<Reminder> GetAllRemindersByUserId(string userId)
        {
            return _reminderRepository.GetAllRemindersByUserId(userId);
        }

        public Reminder GetReminderById(int reminderId)
        {
            var result = _reminderRepository.GetReminderById(reminderId);

            if (result == null)
            {
                throw new ReminderNotFoundException("This reminder id not found");
            }
            return result;
        }

        public bool UpdateReminder(int reminderId, Reminder reminder)
        {
            var result = _reminderRepository.UpdateReminder(reminderId, reminder);

            if (!result)
            {
                throw new ReminderNotFoundException("This reminder id not found");
            }
            return result;
        }
    }
}
