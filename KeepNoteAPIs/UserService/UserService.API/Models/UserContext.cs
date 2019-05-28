using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace UserService.API.Models
{
    public class UserContext:IUserContext
    {
        private readonly IMongoDatabase database;
        MongoClient client;
        public UserContext(IConfiguration configuration)
        {
            client = new MongoClient(configuration.GetSection("MongoDB:ConnectionString").Value);
            database = client.GetDatabase(configuration.GetSection("MongoDB:Database").Value);
        }

        public IMongoCollection<User> Users => database.GetCollection<User>("Users");
    }
}
