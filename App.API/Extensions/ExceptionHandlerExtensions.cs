using App.API.ExceptionHandlers;
using App.API.Exceptions;

namespace App.API.Extensions
{
    public static class ExceptionHandlerExtensions
    {
        public static IServiceCollection AddExceptionHandlerExt(this IServiceCollection services)
        {
            services.AddExceptionHandler<CriticalExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();

            return services;
        }
    }
}