using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace UserService.API.Models
{
    public class User
    {
        [BsonId]
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Contact { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
