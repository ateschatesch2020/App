using App.Application.Contracts.Caching;
using App.Application.Features.Categories;
using App.Application.Features.Categories.Dto;
using App.Application.Features.Products;
using App.Application.Features.Products.Dto;
using App.Domain.Entities;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace App.Application.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            //services.AddScoped<IProductService, ProductService>(); // done with autofac
            //services.AddScoped<ICategoryService, CategoryService>();


            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<CategoryMappingProfile>();
                cfg.AddProfile<ProductMappingProfile>();
            });

            return services;
        }
    }
}