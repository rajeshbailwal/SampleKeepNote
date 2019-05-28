using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using NoteService.API.Models;

namespace NoteService.Test
{
    public class DatabaseFixture : IDisposable
    {
        private IConfigurationRoot configuration;
        public INoteContext context;
        public DatabaseFixture()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

            configuration = builder.Build();
            context = new NoteContext(configuration);
            context.Notes.DeleteMany(Builders<NoteUser>.Filter.Empty);
            context.Notes.InsertMany(new List<NoteUser>
            {
                new NoteUser{
                    UserId="Mukesh", Notes=new List<Note>{
                        new Note { Id=101, Category= new Category{Id=101, Name="Sports", CreatedBy="Mukesh", Description="All about sports", CreationDate=new DateTime() },
                        Content="Sample Note", CreatedBy="Mukesh", Reminders=new List<Reminder>{ new Reminder { Id = 201, Name = "Sports", CreatedBy = "Mukesh", Description = "sports reminder", CreationDate = new DateTime(), Type = "email" } } ,
                        Title="Sample", CreationDate=new DateTime()}
                    }
                },

                new NoteUser{
                    UserId="Sachin", Notes=new List<Note>{
                        new Note { Id=141, Category= new Category{Id=102, Name="Sports", CreatedBy="Sachin", Description="All about sports", CreationDate=new DateTime() },
                        Content="Sample Note", CreatedBy="Sachin", Reminders=new List<Reminder>{ new Reminder { Id = 202, Name = "Sports", CreatedBy = "Sachin", Description = "sports reminder", CreationDate = new DateTime(), Type = "email" } } ,
                        Title="Sample", CreationDate=new DateTime()}
                    }
                }

            });
        }
        public void Dispose()
        {
            context = null;
        }
    }
}
