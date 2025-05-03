using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Petsica.Core.Persistence.EntitiesConfigurations;
using Petsica.Core.Persistence.EntitiesConfigurations.Community;
using Petsica.Core.Persistence.EntitiesConfigurations.Marketplace;
using Petsica.Core.Persistence.EntitiesConfigurations.Messages;
using Petsica.Core.Persistence.EntitiesConfigurations.Pets;
using Petsica.Core.Persistence.EntitiesConfigurations.Services;
using Petsica.Core.Persistence.EntitiesConfigurations.ServicesS;
using Petsica.Core.Persistence.EntitiesConfigurations.Users;
using Petsica.Infrastructure.DataBase.Configurations;
using Petsica.Infrastructure.DBModel;





namespace Petsica.Infrastructure
{

    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
        IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
    {
        #region Tables
        public DbSet<User> Users { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserCommentPost> UserCommentPosts { get; set; }
        public DbSet<UserLikePost> UserLikePosts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<SitterService> Services { get; set; }
        public DbSet<UserRequestService> UserRequestServices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<SellerOrder> SellerOrder { get; set; }

        //public DbSet<Admin> Admins { get; set; }
        //public DbSet<SitterApproval> SitterApprovals { get; set; }
        //public DbSet<SellerApproval> SellerApprovals { get; set; }
        //public DbSet<ClinicApproval> ClinicApprovals { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<ClinicMessageClinic> ClinicMessageClinics { get; set; }
        public DbSet<UserMessageClinic> UserMessageClinics { get; set; }
        public DbSet<UserMessageUser> UserMessageUsers { get; set; }
        public DbSet<UserRequestPet> UserRequestPets { get; set; }
        public DbSet<UserRemindPet> UserRemindPets { get; set; }
        public DbSet<UserFollow> UserFollows { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ///  modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PetConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new UserCommentPostConfiguration());
            modelBuilder.ApplyConfiguration(new UserLikePostConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceConfiguration());
            modelBuilder.ApplyConfiguration(new UserRequestServiceConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CartItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new SellerOrderConfiguration());

            //modelBuilder.ApplyConfiguration(new AdminConfiguration());
            //modelBuilder.ApplyConfiguration(new SitterApprovalConfiguration());
            //modelBuilder.ApplyConfiguration(new SellerApprovalConfiguration());
            //modelBuilder.ApplyConfiguration(new ClinicApprovalConfiguration());
            modelBuilder.ApplyConfiguration(new ClinicConfiguration());
            modelBuilder.ApplyConfiguration(new ClinicMessageClinicConfiguration());
            modelBuilder.ApplyConfiguration(new UserMessageClinicConfiguration());
            modelBuilder.ApplyConfiguration(new UserMessageUserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRequestPetConfiguration());
            modelBuilder.ApplyConfiguration(new UserRemindPetConfiguration());

            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleClaimConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }



}