

namespace Petsica.Core.Persistence.EntitiesConfigurations.Community;
public class UserCommentPostConfiguration : IEntityTypeConfiguration<UserCommentPost>
{
    public void Configure(EntityTypeBuilder<UserCommentPost> builder)
    {
        builder.HasKey(c => c.CommentID);

        builder.Property(c => c.Content)
               .IsRequired()
               .HasMaxLength(250);

        builder.Property(c => c.Date)
               .IsRequired();

        #region Relationships
        builder.HasOne(c => c.User)
               .WithMany(u => u.Comments)
               .HasForeignKey(c => c.UserID);

        builder.HasOne(c => c.Post)
               .WithMany(p => p.Comments)
               .HasForeignKey(c => c.PostID);
        #endregion
    }
}
