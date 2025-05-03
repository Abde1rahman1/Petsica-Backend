
//namespace Petsica.Core.Persistence.EntitiesConfigurations.Pets;
//public class UserRequestPetConfiguration : IEntityTypeConfiguration<UserRequestPet>
//{
//    public void Configure(EntityTypeBuilder<UserRequestPet> builder)
//    {
//        builder.HasKey(r => new { r.PetID, r.UserID });

//        #region Relationships
//        builder.HasOne(r => r.User)
//               .WithMany()
//               .HasForeignKey(r => r.UserID).OnDelete(DeleteBehavior.NoAction);

//        builder.HasOne(r => r.Pet)
//               .WithMany()
//               .HasForeignKey(r => r.PetID).OnDelete(DeleteBehavior.NoAction);
//        #endregion
//    }
//}
