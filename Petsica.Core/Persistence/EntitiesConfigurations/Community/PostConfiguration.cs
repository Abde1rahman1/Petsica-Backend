
namespace Petsica.Core.Persistence.EntitiesConfigurations.Community;
public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(p => p.PostID);

        builder.Property(p => p.Content)
               .IsRequired()
               .HasMaxLength(300);

        builder.Property(p => p.Date)
               .IsRequired();

        #region Relationships
        builder.HasOne(p => p.User)
               .WithMany(u => u.Posts)
               .HasForeignKey(p => p.UserID).OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(p => p.Comments)
               .WithOne(c => c.Post)
               .HasForeignKey(c => c.PostID).OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(p => p.Likes)
               .WithOne(l => l.Post)
               .HasForeignKey(l => l.PostID).OnDelete(DeleteBehavior.NoAction);
        #endregion

    }
}
