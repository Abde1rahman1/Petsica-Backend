using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Petsica.Shared
{
    public static class DepenceyInjectionShared
    {

        public static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)

        {
            services
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}