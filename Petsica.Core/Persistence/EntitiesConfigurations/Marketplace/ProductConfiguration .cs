

namespace Petsica.Core.Persistence.EntitiesConfigurations.Marketplace;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.ProductID);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(25);

        #region Relationships
        builder.HasOne(p => p.Seller)
               .WithMany()
               .HasForeignKey(p => p.SellerID).OnDelete(DeleteBehavior.NoAction);
        #endregion
    }
}
