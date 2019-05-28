using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using UserService.API.Models;

namespace UserService.Test
{
    public class DatabaseFixture:IDisposable
    {
        private IConfigurationRoot configuration;
        public IUserContext context;
        public DatabaseFixture()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

            configuration = builder.Build();
            context = new UserContext(configuration);
            context.Users.DeleteMany(Builders<User>.Filter.Empty);
            context.Users.InsertMany(new List<User>
            {
                new User{ UserId="Mukesh", Name="Mukesh", Password="admin123", Contact="9812345670", AddedDate=new DateTime()},
                new User{ UserId="Sachin", Name="Sachin", Password="admin123", Contact="8987653412", AddedDate=new DateTime()}
            });
        }
        public void Dispose()
        {
            context = null;
        }
    }
}
