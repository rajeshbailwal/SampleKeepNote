using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace NoteService.API.Models
{
    public class Note
    {
        [BsonId]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public Category Category { get; set; }
        public List<Reminder> Reminders { get; set; }
        public List<string> Tags { get; set; }
    }
}
