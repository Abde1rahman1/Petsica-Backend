using Petsica.Core.Const;
using Petsica.Infrastructure.DBModel;

namespace Petsica.Infrastructure.DataBase.Configurations
{

    public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            //Default Data
            builder.HasData([
                new ApplicationRole
            {
                Id = DefaultRoles.Admin.Id,
                Name = DefaultRoles.Admin.Name,
                NormalizedName = DefaultRoles.Admin.Name.ToUpper(),
                ConcurrencyStamp = DefaultRoles.Admin.ConcurrencyStamp
            },
            new ApplicationRole
            {
                Id = DefaultRoles.Member.Id,
                Name = DefaultRoles.Member.Name,
                NormalizedName = DefaultRoles.Member.Name.ToUpper(),
                ConcurrencyStamp = DefaultRoles.Member.ConcurrencyStamp,
                IsDefault = true
            },
            new ApplicationRole
            {
                Id =DefaultRoles.Seller.Id,
                Name = DefaultRoles.Seller.Name,
                NormalizedName= DefaultRoles.Seller.Name.ToUpper(),
                ConcurrencyStamp= DefaultRoles.Seller.ConcurrencyStamp,

            },
            new ApplicationRole
            {
                Id =DefaultRoles.Sitter.Id,
                Name = DefaultRoles.Sitter.Name,
                NormalizedName= DefaultRoles.Sitter.Name.ToUpper(),
                ConcurrencyStamp= DefaultRoles.Sitter.ConcurrencyStamp,

            },
            new ApplicationRole
            {
                Id =DefaultRoles.Clinic.Id,
                Name = DefaultRoles.Clinic.Name,
                NormalizedName= DefaultRoles.Clinic.Name.ToUpper(),
                ConcurrencyStamp= DefaultRoles.Clinic.ConcurrencyStamp,

            }
            ]);
        }
    }
}


