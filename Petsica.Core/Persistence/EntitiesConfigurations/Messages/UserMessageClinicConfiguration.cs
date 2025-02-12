namespace Petsica.Core.Persistence.EntitiesConfigurations.Messages;
public class UserMessageClinicConfiguration : IEntityTypeConfiguration<UserMessageClinic>
{
    public void Configure(EntityTypeBuilder<UserMessageClinic> builder)
    {
        builder.HasKey(m => m.MessageID);

        builder.Property(m => m.Content)
               .IsRequired()
               .HasMaxLength(500);

        #region Relationships
        builder.HasOne(m => m.User)
               .WithMany()
               .HasForeignKey(m => m.UserID).OnDelete(DeleteBehavior.NoAction);
        #endregion
    }
}
