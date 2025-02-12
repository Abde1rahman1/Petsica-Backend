

namespace Petsica.Core.Persistence.EntitiesConfigurations;
public class SellerManageProductConfiguration : IEntityTypeConfiguration<SellerManageProduct>
{
    public void Configure(EntityTypeBuilder<SellerManageProduct> builder)
    {
        builder.HasKey(s => new { s.ProductID, s.SellerID });

        #region Relationships
        builder.HasOne(s => s.Seller)
               .WithMany()
               .HasForeignKey(s => s.SellerID).OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(s => s.Product)
               .WithMany()
               .HasForeignKey(s => s.ProductID).OnDelete(DeleteBehavior.NoAction);
        #endregion
    }
}