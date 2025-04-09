using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Petsica.Infrastructure;
using Petsica.Infrastructure.DBModel;
using Petsica.Service.Services.Authrization;
using Petsica.Shared.Contracts.Authrization;
using System.Text;

namespace Petsica.API
{
    public static class AuthConfigExtensions
    {


        public static IServiceCollection AddAuthConfigDI(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();


            services.AddSingleton<IJwtProvider, JwtProvider>();

            //services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
            services.AddOptions<JwtOptions>()
                .BindConfiguration(JwtOptions.SectionName)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            var jwtSettings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();



            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
                    ValidIssuer = jwtSettings?.Issuer,
                    ValidAudience = jwtSettings?.Audience
                };
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                // options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
            });

            return services;
        }


    }
}
