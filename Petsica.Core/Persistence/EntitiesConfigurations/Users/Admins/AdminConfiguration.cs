namespace Petsica.Core.Persistence.EntitiesConfigurations.Users.Admins;
public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.HasKey(a => a.AdminID);

        builder.Property(a => a.Username)
               .IsRequired()
               .HasMaxLength(15);

        builder.Property(a => a.Password)
               .IsRequired()
               .HasMaxLength(50);

        #region Relationships
        builder.HasMany(a => a.SitterApprovals)
               .WithOne(s => s.Admin)
               .HasForeignKey(s => s.AdminID).OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(a => a.SellerApprovals)
               .WithOne(s => s.Admin)
               .HasForeignKey(s => s.AdminID).OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(a => a.ClinicApprovals)
               .WithOne(c => c.Admin)
               .HasForeignKey(c => c.AdminID).OnDelete(DeleteBehavior.NoAction);
        #endregion
    }
}