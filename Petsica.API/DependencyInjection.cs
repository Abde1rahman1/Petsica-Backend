using Microsoft.EntityFrameworkCore;
using Petsica.Infrastructure;
using Petsica.Service;
using Petsica.Shared;


namespace Petsica.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddControllers();

            //services.AddHybridCache();

            services.AddCors(options =>
                options.AddDefaultPolicy(builder =>
                    builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>()!)
                )
            );


            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddSwaggerServices();
            services.AddMapsterConfig();
            services.AddFluentValidationConfig();

            services.AddAuthConfigDI(configuration);
            services.AddDBConfig(configuration);
            services.AddServiceDI();
            //  services.MailSettings(configuration);
            //  services.AddScoped<IAuthService, AuthService>();

            //  services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            services.AddHttpContextAccessor();

            return services;
        }

        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }


    }
}