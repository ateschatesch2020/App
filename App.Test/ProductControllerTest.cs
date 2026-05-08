using App.API.Controllers;
using App.Application;
using App.Application.Contracts.Caching;
using App.Application.Contracts.Persistence;
using App.Application.Features.Products;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Dto;
using App.Domain.Entities;
using App.Persistence;
using App.Persistence.Products;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace App.Test
{
    public class ProductControllerTest
    {
        protected readonly Mock<IProductService> _mockProductService;
        protected readonly ProductsController _controller;

        private List<Product> _products;
        private ServiceResult<List<ProductDto>> _serviceResult;

        protected DbContextOptions<AppDbContext> _dbContextOptions { get; private set; }

        public void SetContextOptions(DbContextOptions<AppDbContext> options)
        {
            _dbContextOptions = options;
            Seed();
        }   

        public void Seed()
        {
            using(var context = new AppDbContext(_dbContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Categories.Add(new Category() { Name = "Computer Science" });
                context.Categories.Add(new Category() { Name = "Civil Engineering" });
                context.SaveChanges();
                context.Products.Add(new Product() { Name = "Data Structures", Price = 100, Stock = 10, CategoryId = 1 });
                context.Products.Add(new Product() { Name = "Algorithms", Price = 80, Stock = 5, CategoryId = 1 });
            }
        }
        public ProductControllerTest()
        {
            _mockProductService = new Mock<IProductService>();
            _controller = new ProductsController(_mockProductService.Object);

            _products = new List<Product>() {
                new Product { Id = 1, CategoryId = 1, Name="Data Structures", Price=100},
                new Product { Id = 2, CategoryId = 1, Name="Algorithms", Price=80}};

            var list = new List<ProductDto>() {
                new ProductDto(1, "Data Structures", 100, 10, 1),
                new ProductDto(1, "Algorithms", 100, 10, 1), };
            _serviceResult = new ServiceResult<List<ProductDto>>();
            _serviceResult.Data = list;
        }

        /// <summary>
        /// Creates a real ProductService with in-memory DB (without mocks)
        /// </summary>
        protected IProductService CreateRealProductService(AppDbContext context)
        {
            // Setup AutoMapper using ServiceCollection (same way as in production)
            var services = new ServiceCollection();
            services.AddLogging(); // Required by AutoMapper
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<App.Application.Features.Products.ProductMappingProfile>();
            });
            var serviceProvider = services.BuildServiceProvider();
            var mapper = serviceProvider.GetRequiredService<IMapper>();

            // Create real repository and unit of work instances
            var productRepository = new ProductRepository(context);
            var unitOfWork = new UnitOfWork(context);

             
            var validatorMock = new Mock<IValidator<CreateProductRequest>>();
            var cacheServiceMock = new Mock<ICacheService>();

            var httpContextAccessor = new Mock<IHttpContextAccessor>();


            // Create and return real ProductService
            return new ProductService(
                productRepository,
                unitOfWork,
                validatorMock.Object,
                mapper,
                cacheServiceMock.Object, httpContextAccessor.Object
            );
        }


        [Fact]
        public async Task GetAll_ReturnsOk_WithProductList()
        {
            // Arrange
            var products = new List<ProductDto> { new ProductDto(1, "Data Structures",  100, 10,1) };
            var serviceResult = ServiceResult<List<ProductDto>>.Success(products);

            _mockProductService
                .Setup(s => s.GetAllListAsync())
                .ReturnsAsync(serviceResult);

            // Act
            var actionResult = await _controller.GetAll();

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(actionResult);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);

            var returnedResult = Assert.IsType<ServiceResult<List<ProductDto>>>(objectResult.Value);
            Assert.Equal(products, returnedResult.Data);
        }

        [Fact]
        public async Task GetAll_ReturnsNoContent_WhenListIsEmpty()
        {
            // Arrange
            var serviceResult = ServiceResult<List<ProductDto>>.Success(
                null, HttpStatusCode.NoContent);

            _mockProductService
                .Setup(s => s.GetAllListAsync())
                .ReturnsAsync(serviceResult);

            // Act
            var actionResult = await _controller.GetAll();

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async Task GetAll_ServiceThrows_PropagatesException()
        {
            // Arrange
            _mockProductService
                .Setup(s => s.GetAllListAsync())
                .ThrowsAsync(new Exception("DB connection failed"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _controller.GetAll());
        }
    }
}
