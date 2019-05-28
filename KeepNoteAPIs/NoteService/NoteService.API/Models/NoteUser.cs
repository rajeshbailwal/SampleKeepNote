using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace NoteService.API.Models
{
    public class NoteUser
    {
        [BsonId]
        public string UserId { get; set; }
        public List<Note> Notes { get; set; }
    }
}
