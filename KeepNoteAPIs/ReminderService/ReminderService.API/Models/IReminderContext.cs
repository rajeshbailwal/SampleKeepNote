using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace ReminderService.API.Models
{
    public interface IReminderContext
    {
        IMongoCollection<Reminder> Reminders { get; }
    }
}
