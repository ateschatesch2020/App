using App.Application.Contracts.Persistence;
using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Dto;
using App.Application.Features.Categories.Update;
using App.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Features.Categories
{
    public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
    {
        public async Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request)
        {
            var category = await categoryRepository.AnyAsync(x => x.Name == request.Name);

            if (category)
            {
                return ServiceResult<int>.Fail("category name is in db", System.Net.HttpStatusCode.NotFound);
            }
            
            var newCategory = mapper.Map<Category>(request);
            await categoryRepository.AddAsync(newCategory);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult<int>.SuccessAsCreated(newCategory.Id, $"api/categories/{newCategory.Id}");
        }

        public Task<ServiceResult> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<List<CategoryDto>>> GetAllListAsync()
        {
            var categories = await categoryRepository.GetAllAsync();

            #region manuel mapping

            //var categoriesAsDto = categories.Select(p => new CategoryDto(p.Id, p.Name)).ToList();

            #endregion

            var categoriesDto = mapper.Map<List<CategoryDto>>(categories);

            return ServiceResult<List<CategoryDto>>.Success(categoriesDto);
        }

        public async Task<ServiceResult<CategoryDto?>> GetByIdAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if(category is null)
            {
                return ServiceResult<CategoryDto?>.Fail("Category not found", System.Net.HttpStatusCode.NotFound);
            }

            var categoryDto = mapper.Map<CategoryDto>(category);
            return ServiceResult<CategoryDto?>.Success(categoryDto);
        }

        public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int categoryId)
        {
            var category = await categoryRepository.GetCategoryWithProductsAsync(categoryId);
            if(category is null)
            {
                return ServiceResult<CategoryWithProductsDto>.Fail("Category not found", System.Net.HttpStatusCode.NotFound);
            }

            var categoryDto = mapper.Map<CategoryWithProductsDto>(category);

            return ServiceResult<CategoryWithProductsDto>.Success(categoryDto);
        }

        public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync()
        {
            var category = await categoryRepository.GetCategoryWithProductsAsync();

            var categoryDto = mapper.Map<List<CategoryWithProductsDto>>(category);

            return ServiceResult<List<CategoryWithProductsDto>>.Success(categoryDto);
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request)
        {
            var isCategoryNameExist = await categoryRepository.AnyAsync(x => x.Name == request.Name && x.Id != id);

            if (isCategoryNameExist)
            {
                return ServiceResult.Fail("Category name is exist in db", System.Net.HttpStatusCode.BadRequest);
            }

            var category = mapper.Map<Category>(request);
            category.Id = id;

            categoryRepository.Update(category);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(System.Net.HttpStatusCode.NoContent);
        }
    }
}
