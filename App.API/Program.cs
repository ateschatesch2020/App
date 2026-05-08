using App.API.ExceptionHandlers;
using App.API.Extensions;
using App.API.Filters;
using App.API.Modules;
using App.Application.Contracts.Caching;
using App.Application.Extensions;
using App.Caching;
using App.Persistence;
using App.Persistence.Extensions;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CleanApp.API.Extensions;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithFiltersExt().AddExceptionHandlerExt().AddCachingExt();

builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

//builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);

#region identity context
AddIdentityFeature(builder);

#endregion

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));

var app = builder.Build();


// Seed
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    await IdentitySeeder.SeedAsync(userManager, roleManager);
}

app.UseConfigurePipelineExt();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();


app.Run();

static void AddIdentityFeature(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetSection
                  (ConnectionStringOption.Key).Get<ConnectionStringOption>();

    builder.Services.AddDbContext<AppIdentityDbContext>(options =>
         options.UseSqlServer(connectionString!.SqlServer)); // direkt kullan, GetConnectionString() değil

    builder.Services
        .AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<AppIdentityDbContext>()
        .AddDefaultTokenProviders();
}