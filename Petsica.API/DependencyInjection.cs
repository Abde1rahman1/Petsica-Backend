using Microsoft.EntityFrameworkCore;
using Petsica.Infrastructure;
using Petsica.Service;
using Petsica.Service.Health;
using Petsica.Shared;

namespace Petsica.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddControllers();


            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://127.0.0.1:5500")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddSignalR();

            services.AddSwaggerServices();
            services.AddMapsterConfig();
            services.AddFluentValidationConfig();

            services.AddAuthConfigDI(configuration);
            services.AddDBConfig(configuration);
            services.AddServiceDI();

            services.AddProblemDetails();
            services.AddHttpContextAccessor();

            services.AddBackgroundJobsConfig(configuration);


            services.AddHealthChecks()
             .AddSqlServer(name: "database", connectionString: connectionString!)
             .AddHangfire(options => { options.MinimumAvailableServers = 1; })
             .AddCheck<MailProviderHealthCheck>(name: "mail service");

            return services;
        }

        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
    }
}
