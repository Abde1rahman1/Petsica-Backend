using Hangfire;
using MapsterMapper;
using Petsica.Service.Abstractions.Community;
using Petsica.Service.Abstractions.Dashboard;
using Petsica.Service.Abstractions.Marketplace;
using Petsica.Service.Abstractions.Messages;
using Petsica.Service.Abstractions.Pets;
using Petsica.Service.Abstractions.Users;
using Petsica.Service.Service.Community;
using Petsica.Service.Services;
using Petsica.Service.Services.Chat;
using Petsica.Service.Services.Community;
using Petsica.Service.Services.Email;
using Petsica.Service.Services.Marketplace;
using Petsica.Service.Services.Pets;
using Petsica.Service.Services.Users;
using System.Reflection;

namespace Petsica.Service
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddServiceDI(this IServiceCollection services)
        {


            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IPostService, PostService>();

            services.AddScoped<IPetsService, PetsService>();

            services.AddScoped<ILikeService, LikeService>();

            services.AddScoped<ICommentService, CommentService>();

            services.AddScoped<IUserFollow, UserFollowService>();

            services.AddScoped<IEmailSender, EmailService>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<ICartService, CartService>();

            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IClinicChatService, ClinicChatService>();

            services.AddScoped<IUserChatService, UserChatService>();

            services.AddScoped<IDashboardService, DashboardService>();

            services.AddHttpContextAccessor();

            return services;
        }


        public static IServiceCollection AddBackgroundJobsConfig(this IServiceCollection services,
       IConfiguration configuration)
        {
            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection")));

            services.AddHangfireServer();

            return services;
        }
        public static IServiceCollection AddMapsterConfig(this IServiceCollection services)
        {
            var mappingConfig = TypeAdapterConfig.GlobalSettings;
            mappingConfig.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton<IMapper>(new Mapper(mappingConfig));

            return services;


        }
    }
}
