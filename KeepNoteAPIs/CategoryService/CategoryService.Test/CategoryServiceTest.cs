using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using CategoryService.API.Models;
using CategoryService.API.Repository;
using CategoryService.API.Exceptions;
using MongoDB.Driver;

namespace CategoryService.Test
{
    public class CategoryServiceTest
    {
        #region Positive tests
        [Fact]
        public void CreateCategoryShouldReturnCategory()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            Category category = new Category { Id = 121, Name = "Entertainment", CreatedBy = "Sanjeev", Description = "All about entertainment", CreationDate = new DateTime() };
            mockRepo.Setup(repo => repo.CreateCategory(category)).Returns(category);
            var service = new CategoryService.API.Service.CategoryService(mockRepo.Object);

            var actual = service.CreateCategory(category);

            Assert.NotNull(actual);
            Assert.IsAssignableFrom<Category>(actual);
        }

        [Fact]
        public void GetCategoryByUserShouldReturnListOfcategory()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            var userId = "Mukesh";
            mockRepo.Setup(repo => repo.GetAllCategoriesByUserId(userId)).Returns(this.GetCategories());
            var service = new CategoryService.API.Service.CategoryService(mockRepo.Object);

            var actual = service.GetAllCategoriesByUserId(userId);

            Assert.IsAssignableFrom<List<Category>>(actual);
        }

        [Fact]
        public void GetCategoryByIdShouldReturnCategory()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            var Id = 101;
            Category category = new Category { Id = 101, Name = "Sports", CreatedBy = "Mukesh", Description = "All about sports", CreationDate = new DateTime() };

            mockRepo.Setup(repo => repo.GetCategoryById(Id)).Returns(category);
            var service = new CategoryService.API.Service.CategoryService(mockRepo.Object);

            var actual = service.GetCategoryById(Id);

            Assert.IsAssignableFrom<Category>(actual);
            Assert.Equal("Sports", actual.Name);
        }

        [Fact]
        public void DeleteCategoryShouldReturnTrue()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            var Id = 102;

            mockRepo.Setup(repo => repo.DeleteCategory(Id)).Returns(true);
            var service = new CategoryService.API.Service.CategoryService(mockRepo.Object);


            var actual = service.DeleteCategory(Id);

            Assert.True(actual);
        }

        [Fact]
        public void UpdateCategoryShouldReturnTrue()
        {
            int Id = 101;
            Category category = new Category { Id = 101, Name = "Sports", CreatedBy = "Mukesh", Description = "Olympic Games", CreationDate = new DateTime() };

            var mockRepo = new Mock<ICategoryRepository>();

            mockRepo.Setup(repo => repo.UpdateCategory(Id,category)).Returns(true);
            var service = new CategoryService.API.Service.CategoryService(mockRepo.Object);


            var actual = service.UpdateCategory(Id, category);
            Assert.True(actual);
        }

        private List<Category> GetCategories()
        {
            List<Category> categories = new List<Category> {
                new Category{Id=101, Name="Sports", CreatedBy="Mukesh", Description="All about sports", CreationDate=new DateTime() },
                 new Category{Id=102, Name="Politics", CreatedBy="Mukesh", Description="INDIAN politics", CreationDate=new DateTime() }
            } ;

            return categories;
        }

        #endregion Positive tests

        #region Negative tests

        [Fact]
        public void CreateCategoryShouldThrowException()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            Category category = new Category { Id = 101, Name = "Sports", CreatedBy = "Mukesh", Description = "All about sports", CreationDate = new DateTime() };
            mockRepo.Setup(repo => repo.GetCategoryById(101)).Returns(category);
            var service = new CategoryService.API.Service.CategoryService(mockRepo.Object);

            var actual = Assert.Throws<CategoryNotCreatedException>(()=> service.CreateCategory(category));
            Assert.Equal("This category id already exists",actual.Message);
        }

        
        [Fact]
        public void GetCategoryByUserShouldReturnEmptyList()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            var userId = "Nitin";
            mockRepo.Setup(repo => repo.GetAllCategoriesByUserId(userId)).Returns(new List<Category>());
            var service = new CategoryService.API.Service.CategoryService(mockRepo.Object);

            var actual = service.GetAllCategoriesByUserId(userId);

            Assert.IsAssignableFrom<List<Category>>(actual);
            Assert.Empty(actual);
        }

        
        [Fact]
        public void GetCategoryByIdShouldThrowException()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            var Id = 105;
            Category category = null;
            mockRepo.Setup(repo => repo.GetCategoryById(Id)).Returns(category);
            var service = new CategoryService.API.Service.CategoryService(mockRepo.Object);

            var actual =Assert.Throws<CategoryNotFoundException>(()=>  service.GetCategoryById(Id));

            Assert.Equal("This category id not found", actual.Message);
        }

       
        [Fact]
        public void DeleteCategoryShouldThrowException()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            var Id = 105;

            mockRepo.Setup(repo => repo.DeleteCategory(Id)).Returns(false);
            var service = new CategoryService.API.Service.CategoryService(mockRepo.Object);


            var actual = Assert.Throws<CategoryNotFoundException>(()=> service.DeleteCategory(Id));

            Assert.Equal("This category id not found", actual.Message);
        }
        
       [Fact]
       public void UpdateCategoryShouldThrowException()
       {
           int Id = 105;
           Category category = new Category { Id = 105, Name = "Sports", CreatedBy = "Mukesh", Description = "Olympic Games", CreationDate = new DateTime() };

           var mockRepo = new Mock<ICategoryRepository>();

           mockRepo.Setup(repo => repo.UpdateCategory(Id, category)).Returns(false);
           var service = new CategoryService.API.Service.CategoryService(mockRepo.Object);


           var actual = Assert.Throws<CategoryNotFoundException>(() => service.UpdateCategory(Id, category));
            Assert.Equal("This category id not found", actual.Message);
        }

        #endregion Negative tests
    }
}
