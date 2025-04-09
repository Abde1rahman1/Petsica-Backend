

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
               .HasForeignKey(p => p.SellerID)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(p => p.OrderItems)
               .WithOne(oi => oi.Product)
               .HasForeignKey(oi => oi.ProductId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(p => p.CartItems)
               .WithOne(ci => ci.Product)
               .HasForeignKey(ci => ci.ProductId)
               .OnDelete(DeleteBehavior.NoAction);

        #endregion
    }
}
