using System.Linq;
using MongoDB.Driver;
using UserService.API.Models;

namespace UserService.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private IUserContext _dbContext;
        public UserRepository(IUserContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool DeleteUser(string userId)
        {
            DeleteResult actionResult
                = _dbContext.Users.DeleteOne(x => x.UserId == userId);

            return actionResult.IsAcknowledged
                && actionResult.DeletedCount > 0;
        }

        public User GetUserById(string userId)
        {
            return _dbContext.Users.Find(x => x.UserId == userId).FirstOrDefault();
        }

        public User RegisterUser(User user)
        {
            _dbContext.Users.InsertOne(user);

            return _dbContext.Users.Find(x => x.UserId == user.UserId).FirstOrDefault();
        }

        public bool UpdateUser(string userId, User user)
        {
            var item = GetUserById(userId) ?? new User();
            item = user;

            ReplaceOneResult actionResult = _dbContext.Users.ReplaceOne(x => x.UserId.Equals(userId),
                item, new UpdateOptions { IsUpsert = true });

            return actionResult.IsAcknowledged
                && actionResult.ModifiedCount > 0;
        }
    }
}
