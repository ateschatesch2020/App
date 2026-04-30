using App.Application;
using App.Application.Contracts.Persistence;
using App.Persistence;
using Autofac;
using System.Reflection;
using Module = Autofac.Module;

namespace App.API.Modules
{
    public class RepoServiceModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<,>)).As(typeof(IGenericRepository<,>)).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var applicationAssembly = typeof(ApplicationAssembly).Assembly;// we use applicationAssembly class to reach to assembly of the project in order to add all Service ending class to add to DI mechanism in autofac

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, applicationAssembly)
                .Where(t => t.Name.EndsWith("Repository") || t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            //InstancePerLifetimeScope() ensures that a new instance of the registered type is created for each lifetime scope. In web applications, this typically means that a new instance will be created for each HTTP request, which is ideal for services and repositories that may hold state or need to be disposed of after use.
            //InstanceDependancyLifetime() is a more general method that allows you to specify the lifetime of the registered type. It can be used to create singletons, transient instances, or instances that are shared within a specific scope. In contrast,
            //InstancePerLifetimeScope() is specifically designed for scenarios where you want to create a new instance for each lifetime scope, which is common in web applications.
        }
    }
}
