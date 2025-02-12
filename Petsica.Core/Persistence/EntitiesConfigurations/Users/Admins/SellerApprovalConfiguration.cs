namespace Petsica.Core.Persistence.EntitiesConfigurations.Users.Admins;
public class SellerApprovalConfiguration : IEntityTypeConfiguration<SellerApproval>
{
    public void Configure(EntityTypeBuilder<SellerApproval> builder)
    {
        builder.HasKey(s => s.ApprovalID);

        builder.Property(s => s.Status)
               .IsRequired()
               .HasMaxLength(10);

        #region Relationships
        builder.HasOne(s => s.Seller)
               .WithMany()
               .HasForeignKey(s => s.SellerID).OnDelete(DeleteBehavior.NoAction);
        #endregion
    }
}
