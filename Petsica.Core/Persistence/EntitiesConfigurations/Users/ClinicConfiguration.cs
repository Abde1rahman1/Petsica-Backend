namespace Petsica.Core.Persistence.EntitiesConfigurations.Users;
public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
{
    public void Configure(EntityTypeBuilder<Clinic> builder)
    {
        builder.HasKey(c => c.ClinicID);

        builder.Property(c => c.Username)
               .IsRequired()
               .HasMaxLength(15);

        builder.Property(c => c.Password)
               .IsRequired()
               .HasMaxLength(50);

        #region Relationships
        builder.HasMany(c => c.ClinicMessages)
               .WithOne(m => m.Sender)
               .HasForeignKey(m => m.ClinicSenderID);

        builder.HasMany(c => c.UserMessages)
               .WithOne(m => m.Clinic)
               .HasForeignKey(m => m.ClinicID);
        #endregion
    }
}
