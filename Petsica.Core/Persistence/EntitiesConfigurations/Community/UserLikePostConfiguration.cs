
namespace Petsica.Core.Persistence.EntitiesConfigurations;
public class UserLikePostConfiguration : IEntityTypeConfiguration<UserLikePost>
{
	public void Configure(EntityTypeBuilder<UserLikePost> builder)
	{
		builder.HasKey(l => l.LikeID);

		#region Relationships
		builder.HasOne(l => l.User)
			   .WithMany(u => u.Likes)
			   .HasForeignKey(l => l.UserID);

		builder.HasOne(l => l.Post)
			   .WithMany(p => p.Likes)
			   .HasForeignKey(l => l.PostID);
		#endregion
	}
}
