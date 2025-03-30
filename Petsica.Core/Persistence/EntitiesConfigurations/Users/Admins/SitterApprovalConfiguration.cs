//namespace Petsica.Core.Persistence.EntitiesConfigurations.Users.Admins;
//public class SitterApprovalConfiguration : IEntityTypeConfiguration<SitterApproval>
//{
//    public void Configure(EntityTypeBuilder<SitterApproval> builder)
//    {
//        builder.HasKey(s => s.ApprovalID);

//        builder.Property(s => s.Status)
//               .IsRequired()
//               .HasMaxLength(10);

//        #region Relationships
//        builder.HasOne(s => s.Sitter)
//               .WithMany()
//               .HasForeignKey(s => s.SitterID).OnDelete(DeleteBehavior.NoAction);
//        #endregion
//    }
//}