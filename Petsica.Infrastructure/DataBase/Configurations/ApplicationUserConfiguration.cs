using Petsica.Core.Const;
using Petsica.Infrastructure.DBModel;
using Petsica.Shared.Const;

namespace Petsica.Infrastructure.DataBase.Configurations
{
    public partial class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .OwnsMany(x => x.RefreshTokens)
                .ToTable("RefreshTokens")
                .WithOwner()
                .HasForeignKey("UserId");



            //Default Data
            builder.HasData(new ApplicationUser
            {
                Id = DefaultUsers.Admin.Id,

                UserName = DefaultUsers.Admin.Email,
                NormalizedUserName = DefaultUsers.Admin.Email.ToUpper(),
                Email = DefaultUsers.Admin.Email,
                NormalizedEmail = DefaultUsers.Admin.Email.ToUpper(),
                SecurityStamp = DefaultUsers.Admin.SecurityStamp,
                ConcurrencyStamp = DefaultUsers.Admin.ConcurrencyStamp,
                EmailConfirmed = true,
                PasswordHash = DefaultUsers.Admin.PasswordHash,
                Type = RoleName.Admin


            });
        }
    }
}
