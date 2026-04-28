using App.API.ExceptionHandlers;
using App.API.Extensions;
using App.API.Filters;
using App.Application.Contracts.Caching;
using App.Application.Extensions;
using App.Caching;
using App.Persistence;
using App.Persistence.Extensions;
using CleanApp.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithFiltersExt().AddExceptionHandlerExt().AddCachingExt();


//builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);


var app = builder.Build();

app.UseConfigurePipelineExt();

app.MapControllers();

app.Run();
