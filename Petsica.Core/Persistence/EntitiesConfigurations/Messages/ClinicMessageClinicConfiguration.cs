namespace Petsica.Core.Persistence.EntitiesConfigurations.Messages;
public class ClinicMessageClinicConfiguration : IEntityTypeConfiguration<ClinicMessageClinic>
{
    public void Configure(EntityTypeBuilder<ClinicMessageClinic> builder)
    {
        builder.HasKey(m => m.MessageID);

        builder.Property(m => m.Content)
               .IsRequired()
               .HasMaxLength(500);

        #region Relationships
        builder.HasOne(m => m.Receiver)
               .WithMany()
               .HasForeignKey(m => m.ClinicReceiverID);
        #endregion
    }
}