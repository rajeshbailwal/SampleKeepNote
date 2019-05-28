using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CategoryService.API.Models
{
    public class CategoryContext : ICategoryContext
    {
        private readonly IMongoDatabase mongoDatabase;
        MongoClient mongoClient;

        public CategoryContext(IConfiguration configuration)
        {
            //var constr = Environment.GetEnvironmentVariable("Mongo_DB");
            //mongoClient = new MongoClient(constr);

            mongoClient = new MongoClient(configuration.GetSection("MongoDB:ConnectionString").Value);
            
            mongoDatabase = mongoClient.GetDatabase(configuration.GetSection("MongoDB:Database").Value);
        }

        public IMongoCollection<Category> Categories => mongoDatabase.GetCollection<Category>("Categories");
    }
}
