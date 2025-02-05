using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Petsica.Shared
{
    public static class DepenceyInjectionShared
    {
<<<<<<< HEAD
        public static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
=======
        private static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
>>>>>>> 329df3e52952832db9600b6bd3928ae61f3da4aa
        {
            services
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}