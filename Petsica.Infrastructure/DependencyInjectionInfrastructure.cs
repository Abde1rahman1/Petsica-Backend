﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Petsica.Infrastructure
{
    public static class DependencyInjectionInfrastructure
    {
        public static IServiceCollection AddDBConfig(this IServiceCollection services,
                IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
               throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));

            services.AddHttpContextAccessor();

            return services;
        }

        //public static IServiceCollection AddMapsterConfig(this IServiceCollection services)
        //{
        //    var mappingConfig = TypeAdapterConfig.GlobalSettings;
        //    mappingConfig.Scan(Assembly.GetExecutingAssembly());

        //    new MappingConfigurations().Register(mappingConfig);

        //    services.AddSingleton<IMapper>(new Mapper(mappingConfig));

        //    return services;
        //}

    }
}