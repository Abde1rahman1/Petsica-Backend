

namespace Petsica.Core.Persistence.EntitiesConfigurations.Pets;
public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.HasKey(p => p.PetID);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(15);

        builder.Property(p => p.Species)
               .IsRequired()
               .HasMaxLength(15);

        #region Relationships
        builder.HasOne(p => p.User)
               .WithMany(u => u.Pets)
               .HasForeignKey(p => p.UserID).OnDelete(DeleteBehavior.NoAction);
        #endregion
    }
}
