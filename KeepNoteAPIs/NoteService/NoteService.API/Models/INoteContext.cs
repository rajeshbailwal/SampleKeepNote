using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace NoteService.API.Models
{
    public interface INoteContext
    {
        IMongoCollection<NoteUser> Notes { get; }
    }
}
