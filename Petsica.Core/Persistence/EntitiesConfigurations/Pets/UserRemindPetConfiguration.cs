

namespace Petsica.Core.Persistence.EntitiesConfigurations.Pets;
public class UserRemindPetConfiguration : IEntityTypeConfiguration<UserRemindPet>
{
    public void Configure(EntityTypeBuilder<UserRemindPet> builder)
    {
        builder.HasKey(b => b.UserRemindPetID);

        #region Relationships
        builder.HasOne(r => r.User)
               .WithMany()
               .HasForeignKey(r => r.UserID)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(r => r.Pet)
               .WithMany()
               .HasForeignKey(r => r.PetID)
               .OnDelete(DeleteBehavior.NoAction);
        #endregion
    }
}
