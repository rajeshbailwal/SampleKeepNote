using CategoryService.API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace CategoryService.API.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ICategoryContext _categoryDbContext;

        public CategoryRepository(ICategoryContext categoryDbContext)
        {
            _categoryDbContext = categoryDbContext;
        }

        public Category CreateCategory(Category category)
        {
            category.Id = FindMaxId() + 1;

            _categoryDbContext.Categories.InsertOne(category);

            return _categoryDbContext.Categories.Find(x => x.Id == category.Id).FirstOrDefault();
        }

        public bool DeleteCategory(int categoryId)
        {
            DeleteResult actionResult
                = _categoryDbContext.Categories.DeleteOne(x => x.Id == categoryId);

            return actionResult.IsAcknowledged
                && actionResult.DeletedCount > 0;
        }

        public List<Category> GetAllCategoriesByUserId(string userId)
        {
            return _categoryDbContext.Categories.Find(x => x.CreatedBy.ToLower() == userId.ToLower()).ToList();
        }

        public List<Category> GetAllCategories()
        {
            return _categoryDbContext.Categories.Find(_ => true).ToList();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _categoryDbContext.Categories.Find(x => x.Id == categoryId).FirstOrDefault();
        }

        public Category GetCategoryByName(string name)
        {
            return _categoryDbContext.Categories.Find(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
        }

        public bool UpdateCategory(int categoryId, Category category)
        {
            var item = GetCategoryById(categoryId) ?? new Category();
            item = category;

            return Update(categoryId, item);
        }

        private bool Update(int categoryId, Category category)
        {
            ReplaceOneResult actionResult = _categoryDbContext.Categories.ReplaceOne(x => x.Id.Equals(categoryId),
                category, new UpdateOptions { IsUpsert = true });

            return actionResult.IsAcknowledged
                && actionResult.ModifiedCount > 0;
        }

        private int FindMaxId()
        {
            //var filter = Builders<NoteUser>.Filter.And(
            //   Builders<NoteUser>.Filter.Where(x => x.UserId == userId)
            //   );

            //var sort = Builders<NoteUser>.Sort.Descending("Notes.Id");

            //var result = _notesDbContext.Notes.Find(filter).Sort(sort);


            //var maxId = _categoryDbContext.Categories.AsQueryable().Aggregate();

            var records = _categoryDbContext.Categories.AsQueryable().FirstOrDefault();

            if (records == null)
            {
                return 0;
            }

            return _categoryDbContext.Categories.AsQueryable().Max(x=>x.Id);
        }
    }
}
