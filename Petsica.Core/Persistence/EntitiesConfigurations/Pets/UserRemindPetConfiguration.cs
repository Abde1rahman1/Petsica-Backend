

namespace Petsica.Core.Persistence.EntitiesConfigurations.Pets;
public class UserRemindPetConfiguration : IEntityTypeConfiguration<UserRemindPet>
{
	public void Configure(EntityTypeBuilder<UserRemindPet> builder)
	{
		builder.HasKey(r => new { r.PetID, r.UserID });

		#region Relationships
		builder.HasOne(r => r.User)
			   .WithMany()
			   .HasForeignKey(r => r.UserID);

		builder.HasOne(r => r.Pet)
			   .WithMany()
			   .HasForeignKey(r => r.PetID);
		#endregion
	}
}
