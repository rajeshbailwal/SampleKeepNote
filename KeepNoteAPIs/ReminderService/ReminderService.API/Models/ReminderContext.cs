using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ReminderService.API.Models
{
    public class ReminderContext:IReminderContext
    {
        private readonly IMongoDatabase database;
        MongoClient client;

        public ReminderContext(IConfiguration configuration)
        {
            client = new MongoClient(configuration.GetSection("MongoDB:ConnectionString").Value);
            //var constr = Environment.GetEnvironmentVariable("Mongo_DB");
            //client = new MongoClient(constr);
            database = client.GetDatabase(configuration.GetSection("MongoDB:Database").Value);
        }

        public IMongoCollection<Reminder> Reminders => database.GetCollection<Reminder>("Reminders");
    }
}

