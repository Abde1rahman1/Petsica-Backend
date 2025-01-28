

namespace Petsica.Core.Persistence.EntitiesConfigurations.Users;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.UserID);

        builder.Property(u => u.Username)
               .IsRequired()
               .HasMaxLength(15);

        builder.Property(u => u.Email)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(u => u.Password)
               .IsRequired()
               .HasMaxLength(50);

        #region Relationships
        builder.HasMany(u => u.Pets)
               .WithOne(p => p.User)
               .HasForeignKey(p => p.UserID);

        builder.HasMany(u => u.Posts)
               .WithOne(p => p.User)
               .HasForeignKey(p => p.UserID);

        builder.HasMany(u => u.Comments)
               .WithOne(c => c.User)
               .HasForeignKey(c => c.UserID);

        builder.HasMany(u => u.Likes)
               .WithOne(l => l.User)
               .HasForeignKey(l => l.UserID);

        builder.HasMany(u => u.Orders)
               .WithOne(o => o.User)
               .HasForeignKey(o => o.UserID);

        builder.HasMany(u => u.RequestedServices)
               .WithOne(r => r.User)
               .HasForeignKey(r => r.UserID);
        #endregion
    }
}
