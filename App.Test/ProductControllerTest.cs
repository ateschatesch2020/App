using App.API.Controllers;
using App.Application;
using App.Application.Contracts.Persistence;
using App.Application.Features.Products;
using App.Application.Features.Products.Dto;
using App.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace App.Test
{
    public class ProductControllerTest
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductsController _controller;

        private List<Product> _products;
        private ServiceResult<List<ProductDto>> _serviceResult;

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
