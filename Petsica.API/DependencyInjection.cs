﻿using Microsoft.EntityFrameworkCore;
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
            services.AddServiceDI(configuration);
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

        //private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
        //{
        //    var mappingconfig = TypeAdapterConfig.GlobalSettings;
        //    mappingconfig.Scan(Assembly.GetExecutingAssembly());

        //    services.AddSingleton<IMapper>(new Mapper(mappingconfig));

        //    return services;
        //}

        //private static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
        //{
        //    services
        //        .AddFluentValidationAutoValidation()
        //        .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        //    return services;
        //}

        //private static IServiceCollection AddAuthConfig(this IServiceCollection services,
        //    IConfiguration configuration)
        //{
        //    services.AddIdentity<ApplicationUser, IdentityRole>()
        //      .AddEntityFrameworkStores<ApplicationDbContext>()
        //      .AddDefaultTokenProviders();


        //    services.AddSingleton<IJwtProvider, JwtProvider>();

        //    //services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        //    services.AddOptions<JwtOptions>()
        //        .BindConfiguration(JwtOptions.SectionName)
        //        .ValidateDataAnnotations()
        //        .ValidateOnStart();

        //    var jwtSettings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

        //    services.AddAuthentication(options =>
        //    {
        //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    })
        //    .AddJwtBearer(o =>
        //    {
        //        o.SaveToken = true;
        //        o.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            ValidateIssuer = true,
        //            ValidateAudience = true,
        //            ValidateLifetime = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
        //            ValidIssuer = jwtSettings?.Issuer,
        //            ValidAudience = jwtSettings?.Audience
        //        };
        //    });

        //    services.Configure<IdentityOptions>(options =>
        //    {
        //        options.Password.RequiredLength = 8;
        //        //options.SignIn.RequireConfirmedEmail = true;
        //        options.User.RequireUniqueEmail = true;
        //    });

        //    return services;
        //}
    }
}