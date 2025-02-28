
using Petsica.Service.Abstractions.Community;
using Petsica.Service.Abstractions.Users;
using Petsica.Service.Service.Community;
using Petsica.Service.Service.Users;


namespace Petsica.Service
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddServiceDI(this IServiceCollection services,
            IConfiguration configuration)
        {


            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IPostService, PostService>();



            return services;
        }



    }
}
