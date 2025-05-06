
namespace Petsica.Core.Persistence.EntitiesConfigurations.Community;

public class UserFollowConfiguration : IEntityTypeConfiguration<UserFollow>
{
    public void Configure(EntityTypeBuilder<UserFollow> builder)
    {
        builder.HasKey(p => p.Id);

        #region Relationships
        builder.HasOne(p => p.User)
               .WithMany(u => u.UserFollowers)
               .HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.NoAction);


        #endregion

    }
}