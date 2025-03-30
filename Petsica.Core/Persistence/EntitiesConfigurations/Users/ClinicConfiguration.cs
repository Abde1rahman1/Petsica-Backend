namespace Petsica.Core.Persistence.EntitiesConfigurations.Users;
public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
{
    public void Configure(EntityTypeBuilder<Clinic> builder)
    {
        builder.HasKey(c => c.ClinicID);


        #region Relationships
        builder.HasMany(c => c.ClinicMessages)
               .WithOne(m => m.Sender)
               .HasForeignKey(m => m.ClinicSenderID).OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(c => c.UserMessages)
               .WithOne(m => m.Clinic)
               .HasForeignKey(m => m.ClinicID).OnDelete(DeleteBehavior.NoAction);
        #endregion
    }
}
