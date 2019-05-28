using MongoDB.Driver;
using NoteService.API.Models;
using System.Collections.Generic;
using System.Linq;


namespace NoteService.API.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly INoteContext _notesDbContext;

        public NoteRepository(INoteContext notesDbContext)
        {
            _notesDbContext = notesDbContext;
        }

        public NoteUser CreateNote(NoteUser noteUser)
        {
            FilterDefinition<NoteUser> filterDefinition = Builders<NoteUser>.Filter.Eq(user => user.UserId, noteUser.UserId);

            NoteUser note = _notesDbContext.Notes.Find(filterDefinition).FirstOrDefault();

            if(note==null || note.Notes.Count==0)
            {
                noteUser.Notes[0].Id = 1;
            }
            else
            {
                noteUser.Notes[0].Id = FindMaxId(noteUser.UserId) + 1;
            }

            if (note == null)
            {
                _notesDbContext.Notes.InsertOne(noteUser);
                return _notesDbContext.Notes.Find(filterDefinition).FirstOrDefault();
            }
            else
            {
                Note note1 = noteUser.Notes.FirstOrDefault();
                var filter = Builders<NoteUser>.Filter.Where(ntusr => ntusr.UserId.ToLower() == noteUser.UserId.ToLower());
                var update = Builders<NoteUser>.Update
                    .Push(f => f.Notes, note1);

                return _notesDbContext.Notes.FindOneAndUpdate(filter, update);
            }
        }

        public bool DeleteNote(string userId, int noteId)
        {
            var update = Builders<NoteUser>.Update.PullFilter(p => p.Notes,
                                                f => f.Id == noteId);
            var result = _notesDbContext.Notes.FindOneAndUpdateAsync(p => p.UserId.ToLower() == userId.ToLower(), update).Result;

            if (result == null)
                return false;

            return true;
        }

        public List<Note> FindByUserId(string userId)
        {
            //var r = _notesDbContext.Notes.Find(_ => true).ToList();
            var result = _notesDbContext.Notes.Find(x => x.UserId.ToLower() == userId.ToLower()).FirstOrDefault();

            if (result == null)
            {
                return new List<Note>();
            }
            return result.Notes;
        }

        public Note GetNoteById(string userId, int noteId)
        {
            var filter = Builders<NoteUser>.Filter.And(
               Builders<NoteUser>.Filter.Where(x => x.UserId.ToLower() == userId.ToLower()),
               Builders<NoteUser>.Filter.ElemMatch(x => x.Notes, c => c.Id == noteId));

            var zooWithAnimalFilter = Builders<NoteUser>.Filter.ElemMatch(z => z.Notes, a => a.Id == noteId);

            var result = _notesDbContext.Notes.Aggregate()
                .Match(filter)
                .Project<Note>(
                    Builders<NoteUser>.Projection.Expression<Note>(z =>
                        z.Notes.FirstOrDefault(a => a.Id == noteId)))
                .FirstOrDefault(); // or .ToList() to return multiple

            return result;

        }

        public NoteUser UpdateNote(int noteId, string userId, Note note)
        {

            var filter = Builders<NoteUser>.Filter;
            var userIdNoteIdFilter = filter.And(
              filter.Eq(x => x.UserId, userId),
              filter.ElemMatch(x => x.Notes, c => c.Id == noteId));

            var update = Builders<NoteUser>.Update;
            var noteUpdate = update.Set(x => x.Notes[-1], note);
            UpdateOptions options = new UpdateOptions { IsUpsert = true };

            var result = _notesDbContext.Notes.UpdateOneAsync(userIdNoteIdFilter, noteUpdate, options);

            return _notesDbContext.Notes.Find(x => x.UserId.ToLower() == userId.ToLower()).FirstOrDefault();
        }

        private int FindMaxId(string userId)
        {
            //var filter = Builders<NoteUser>.Filter.And(
            //   Builders<NoteUser>.Filter.Where(x => x.UserId == userId)
            //   );

            //var sort = Builders<NoteUser>.Sort.Descending("Notes.Id");

            //var result = _notesDbContext.Notes.Find(filter).Sort(sort);


            var groupTravelItemsByCity = _notesDbContext.Notes.AsQueryable().Where(w => w.UserId.ToLower() == userId.ToLower())
                .Select(c => new
                {
                    maxId = c.Notes.Max(x => x.Id)
                });

            return groupTravelItemsByCity == null ? 0 : groupTravelItemsByCity.FirstOrDefault().maxId;
        }
    }
}

