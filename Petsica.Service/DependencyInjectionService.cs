using Petsica.Service.Abstractions.Community;
using Petsica.Service.Abstractions.Marketplace;
using Petsica.Service.Abstractions.Messages;
using Petsica.Service.Abstractions.Pets;
using Petsica.Service.Abstractions.Users;
using Petsica.Service.Service.Community;
using Petsica.Service.Services.Chat;
using Petsica.Service.Services.Community;
using Petsica.Service.Services.Email;
using Petsica.Service.Services.Marketplace;
using Petsica.Service.Services.Pets;
using Petsica.Service.Services.Users;

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

			//services.AddScoped<IClinicChatService, ClinicChatService>();

			services.AddScoped<IUserChatService, UserChatService>();

			services.AddHttpContextAccessor();

            return services;
        }


        //public static IServiceCollection MailSettings(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddOptions<MailSettings>()
        //          .BindConfiguration(nameof(MailSettings))
        //          .ValidateDataAnnotations()
        //          .ValidateOnStart();




        //    return services;
        //}
    }
}
