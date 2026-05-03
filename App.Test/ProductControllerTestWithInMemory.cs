using App.API.Controllers;
using App.Application;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Dto;
using App.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace App.Test
{
    public class ProductControllerTestWithInMemory : ProductControllerTest
    {
        public ProductControllerTestWithInMemory()
        {
            SetContextOptions(new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<Persistence.AppDbContext>()
                .UseInMemoryDatabase("CleanArchitectureInMemoryDB").Options);
        }

        /// <summary>
        /// Test that creates a product using REAL service (no mocks), with in-memory database
        /// </summary>
        [Fact]
        public async Task Create_Product_ReturnsCreated()
        {
            // Arrange
            var newProductName = "New Product in InMemoryDB";
            var newProductPrice = 50.0M;
            var newProductStock = 20;
            int categoryId = 0;

            using (var context = new Persistence.AppDbContext(_dbContextOptions))
            {
                // Get existing category from seeded data
                var category = context.Categories.First();
                categoryId = category.Id;

                var request = new CreateProductRequest(newProductName, newProductPrice, newProductStock, categoryId);

                // Create REAL ProductService (not mocked) with in-memory DB context
                var realProductService = CreateRealProductService(context);

                // Create controller with real service
                var controller = new ProductsController(realProductService);

                // Act
                var actionResult = await controller.Create(request);

                // Assert - Check response
                var createdResult = Assert.IsType<CreatedResult>(actionResult);
                Assert.Equal((int)HttpStatusCode.Created, createdResult.StatusCode);
                Assert.NotNull(createdResult.Value);

                // Assert - Check response body (ServiceResult<CreateProductResponse>)
                var responseServiceResult = Assert.IsType<ServiceResult<CreateProductResponse>>(createdResult.Value);
                Assert.NotNull(responseServiceResult.Data);
                Assert.True(responseServiceResult.Data.Id > 0); // Product should have been assigned an ID
            }

            // Verify - Check that product was actually saved to the database
            using (var context = new Persistence.AppDbContext(_dbContextOptions))
            {
                // Product name is converted to lowercase by ProductMappingProfile
                var savedProduct = context.Products.FirstOrDefault(p => p.Name == newProductName.ToLowerInvariant());

                Assert.NotNull(savedProduct);
                Assert.Equal(newProductName.ToLowerInvariant(), savedProduct.Name);
                Assert.Equal(newProductPrice, savedProduct.Price);
                Assert.Equal(newProductStock, savedProduct.Stock);
                Assert.Equal(categoryId, savedProduct.CategoryId);
            }
        }
    }
}
