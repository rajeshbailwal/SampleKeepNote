using System;
using System.Collections.Generic;
using System.Text;
using CategoryService.API.Models;
using CategoryService.API.Repository;
using Xunit;

namespace CategoryService.Test
{
    public class CategoryRepositoryTest:IClassFixture<DatabaseFixture>
    {
        DatabaseFixture fixture;
        private ICategoryRepository repository;

        public CategoryRepositoryTest(DatabaseFixture _fixture)
        {
            fixture = _fixture;
            repository = new CategoryRepository(fixture.context);
        }
        [Fact]
        public void CreateCategoryShouldReturnCategory()
        {
            Category category = new Category { Id = 121, Name = "Entertainment", CreatedBy = "Sanjeev", Description = "All about entertainment", CreationDate = new DateTime() };

            var actual = repository.CreateCategory(category);
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<Category>(actual);
        }

        [Fact]
        public void GetCategoryByUserShouldReturnListOfcategory()
        {
            var actual = repository.GetAllCategoriesByUserId("Mukesh");

            Assert.IsAssignableFrom<List<Category>>(actual);
        }

        [Fact]
        public void GetCategoryByIdShouldReturnCategory()
        {
            var actual = repository.GetCategoryById(101);

            Assert.IsAssignableFrom<Category>(actual);
            Assert.Equal("Sports", actual.Name);
        }

        [Fact]
        public void DeleteCategoryShouldReturnTrue()
        {
            var actual = repository.DeleteCategory(102);

            Assert.True(actual);
        }

        [Fact]
        public void UpdateCategoryShouldReturnTrue()
        {
            Category category = new Category { Id = 101, Name = "Soprts", CreatedBy = "Mukesh", Description = "Olympic Games", CreationDate = new DateTime() };

            var actual = repository.UpdateCategory(101, category);
            Assert.True(actual);
        }
    }
}
