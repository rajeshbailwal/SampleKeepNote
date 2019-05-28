using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace NoteService.API.Models
{
    public class NoteContext : INoteContext
    {
        private readonly IMongoDatabase database;
        MongoClient client;

        public NoteContext(IConfiguration configuration)
        {
            client = new MongoClient(configuration.GetSection("MongoDB:ConnectionString").Value);

            //var constr = Environment.GetEnvironmentVariable("Mongo_DB");
            //client = new MongoClient(constr);
            database = client.GetDatabase(configuration.GetSection("MongoDB:Database").Value);
        }

        public IMongoCollection<NoteUser> Notes => database.GetCollection<NoteUser>("Notes");
        //public IMongoCollection<Note> Note => database.GetCollection<Note>("Note");
        //public IMongoCollection<Reminder> Reminder => database.GetCollection<Reminder>("Reminder");
    }
}
