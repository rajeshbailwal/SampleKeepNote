using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using CategoryService.API.Service;
using CategoryService.API.Models;
using CategoryService.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CategoryService.Test
{
    public class CategoryControllerTest
    {
        [Fact]
        public void GetByCategoryIdShouldReturnOk()
        {
            int categoryId = 101;
            Category category = new Category { Id = 101, Name = "Sports", CreatedBy = "Mukesh", Description = "All about sports", CreationDate = new DateTime() };
            var mockService = new Mock<ICategoryService>();
            mockService.Setup(service => service.GetCategoryById(categoryId)).Returns(category);
            var controller = new CategoryController(mockService.Object);

            var actual = controller.Get(categoryId);

            var actionReult = Assert.IsType<OkObjectResult>(actual);
            Assert.IsAssignableFrom<Category>(actionReult.Value);
        }

        [Fact]
        public void GetByUserIdShouldReturnAList()
        {
            string userId = "Mukesh";
            
            var mockService = new Mock<ICategoryService>();
            mockService.Setup(service => service.GetAllCategoriesByUserId(userId)).Returns(this.GetCategories());
            var controller = new CategoryController(mockService.Object);

            var actual = controller.Get(userId);

            var actionReult = Assert.IsType<OkObjectResult>(actual);
            Assert.IsAssignableFrom<List<Category>>(actionReult.Value);
        }

        [Fact]
        public void DeleteShouldReturnOK()
        {
            var mockService = new Mock<ICategoryService>();
            //Note note = new Note { Title = "Sample", NoteId = 1 };
            mockService.Setup(service => service.DeleteCategory(101)).Returns(true);
            var controller = new CategoryController(mockService.Object);

            var actual = controller.Delete(101);

            var actionReult = Assert.IsType<OkObjectResult>(actual);
            var actualValue = actionReult.Value;
            var expected = true;
            Assert.Equal(expected, actualValue);
        }

        [Fact]
        public void POSTShouldReturnCreated()
        {
            var mockService = new Mock<ICategoryService>();
            Category category = new Category { Id = 101, Name = "Sports", CreatedBy = "Mukesh", Description = "All about sports", CreationDate = new DateTime() };

            mockService.Setup(service => service.CreateCategory(category)).Returns(category);
            var controller = new CategoryController(mockService.Object);

            var actual = controller.Post(category.CreatedBy,category);

            var actionReult = Assert.IsType<CreatedResult>(actual);
            var actualValue = actionReult.Value;
            Assert.IsAssignableFrom<Category>(actualValue);
        }
        private List<Category> GetCategories()
        {
            List<Category> category = new List<Category> { new Category { Id = 101, Name = "Sports", CreatedBy = "Mukesh", Description = "All about sports", CreationDate = new DateTime() } };

            return category;
        }
    }
}
