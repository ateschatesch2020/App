using App.Application.Contracts.Caching;
using App.Application.Contracts.Persistence;
using App.Application.DesignPatterns.CommandPattern;
using App.Application.DesignPatterns.CommandPattern.Commands;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Dto;
using App.Application.Features.Products.Update;
using App.Application.Features.Products.UpdateStock;
using App.Domain.Entities;
using AutoMapper;
using DocumentFormat.OpenXml.Office2016.Excel;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;

namespace App.Application.Features.Products
{
    public class ProductService(IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            IValidator<CreateProductRequest> createValidator,
            IMapper mapper,
            ICacheService cacheService,
            IHttpContextAccessor httpContextAccessor) : IProductService
    {
        private const string ProductListCacheKey = "ProductListCacheKey";

        public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
        {
            // 2.way manuel async validation with repository
            var anyProduct = await productRepository.AnyAsync(x => x.Name == request.Name);

            if (anyProduct)
            {
                return ServiceResult<CreateProductResponse>.Fail(new List<string> { "there is already this product in db" });
            }
            // 3.way validation with fluent validation
            //var validationResult = await createValidator.ValidateAsync(request);
            //if(!validationResult.IsValid)
            //{
            //    var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            //    return ServiceResult<CreateProductResponse>.Fail(errors);
            //}

            var product = mapper.Map<Product>(request);

            await productRepository.AddAsync(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id), $"api/product/{product.Id}");
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            //if(product is null)
            //{
            //    return ServiceResult.Fail("there is no product with this id", System.Net.HttpStatusCode.NotFound);
            //}

            productRepository.Delete(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(System.Net.HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
        {

            //cache aside design pattern:
            // any cache? from db, add cache


            var productListAsCached = await cacheService.GetAsync<List<ProductDto>>(ProductListCacheKey);

            if (productListAsCached is not null) return ServiceResult<List<ProductDto>>.Success(productListAsCached);

            var products = await productRepository.GetAllAsync();

            var productDtos = mapper.Map<List<ProductDto>>(products);

            await cacheService.AddAsync(ProductListCacheKey, productDtos, TimeSpan.FromMinutes(1));

            return ServiceResult<List<ProductDto>>.Success(productDtos);
        }

        public async Task<ServiceResult<List<ProductDto?>>> GetPagedAllListAsync(int pageNumber, int pageSize)
        {
            //1-10 first 10 skip(0).Take(10)
            //11-20 skip(10).Take(10)
            //21-30 skip(20).Take(10)

            var products = await productRepository.GetAllPagedAsync(pageNumber, pageSize);

            var productDtos = mapper.Map<List<ProductDto>>(products);
            return ServiceResult<List<ProductDto>>.Success(productDtos)!;
        }

        public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return ServiceResult<ProductDto?>.Fail("there is no product with this id", System.Net.HttpStatusCode.NotFound);
            }

            var productDto = mapper.Map<ProductDto>(product);

            return ServiceResult<ProductDto>.Success(productDto)!;
        }

        public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
        {
            var products = await productRepository.GetTopPriceProductsAsync(count);

            var productDtos = mapper.Map<List<ProductDto>>(products);

            return new ServiceResult<List<ProductDto>>
            {
                Data = productDtos
            };
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
        {
            var isProductNameExist = await productRepository.AnyAsync(x => x.Name == request.Name && x.Id != id);

            if (isProductNameExist)
            {
                return ServiceResult.Fail("there is already this product in db", System.Net.HttpStatusCode.BadRequest);
            }

            var product = mapper.Map<Product>(request);
            product.Id = id;

            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(System.Net.HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)
        {
            var product = productRepository.GetByIdAsync(request.ProductId).Result;

            if (product is null)
            {
                return ServiceResult.Fail("there is no product with this id", System.Net.HttpStatusCode.NotFound);
            }

            product.Stock = request.Quantity;
            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(System.Net.HttpStatusCode.NoContent);
        }

        public async Task<IActionResult> CreateFile(int type)
        {
            var products = await productRepository.GetAllAsync();

            FileCreateInvoker fileCreateInvoker = new();

            EnumFileType fileType = (EnumFileType)type;

            switch (fileType)
            {
                case EnumFileType.Excel:
                    ExcelFile<Product> excelFile = new(products);

                    fileCreateInvoker.SetCommand(new CreateExcelTableActionCommand<Product>(excelFile));

                    break;

                case EnumFileType.Pdf:
                    PdfFile<Product> pdfFile = new(products, httpContextAccessor.HttpContext);
                    fileCreateInvoker.SetCommand(new CreatePdfTableActionCommand<Product>(pdfFile));
                    break;

                default:
                    break;
            }

            return fileCreateInvoker.CreateFile();
        }

    }
}
