namespace Petsica.Core.Persistence.EntitiesConfigurations.Messages;
public class UserMessageUserConfiguration : IEntityTypeConfiguration<UserMessageUser>
{
    public void Configure(EntityTypeBuilder<UserMessageUser> builder)
    {
        builder
           .HasKey(m => m.MessageID);

        builder
           .Property(m => m.Content)
           .IsRequired()
           .HasMaxLength(500);
        builder
               .HasOne(m => m.Receiver)
               .WithMany()
               .HasForeignKey(m => m.UserReceiverID)
               .OnDelete(DeleteBehavior.NoAction);

        builder
               .HasOne(m => m.Sender)
               .WithMany()
               .HasForeignKey(m => m.UserSenderID)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
