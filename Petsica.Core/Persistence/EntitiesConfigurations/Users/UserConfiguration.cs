

namespace Petsica.Core.Persistence.EntitiesConfigurations.Users;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.UserID);

        #region Relationships
        builder.HasMany(u => u.Pets)
               .WithOne(p => p.User)
               .HasForeignKey(p => p.UserID).OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(u => u.Posts)
               .WithOne(p => p.User)
               .HasForeignKey(p => p.UserID).OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(u => u.Comments)
               .WithOne(c => c.User)
               .HasForeignKey(c => c.UserID).OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(u => u.Likes)
               .WithOne(l => l.User)
               .HasForeignKey(l => l.UserID).OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(u => u.Orders)
               .WithOne(o => o.User)
               .HasForeignKey(o => o.UserID).OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(u => u.RequestedServices)
               .WithOne(r => r.User)
               .HasForeignKey(r => r.UserID).OnDelete(DeleteBehavior.NoAction);
        #endregion
    }
}
