
namespace Petsica.Core.Persistence.EntitiesConfigurations.Marketplace;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.OrderID);

        builder.Property(o => o.TotalPrice)
               .HasPrecision(18, 2);

        builder.Property(o => o.Address)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(o => o.PhoneNumber)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(o => o.UserID)
               .IsRequired();

        #region Relationships
        builder.HasOne(o => o.User)
               .WithMany(u => u.Orders)
               .HasForeignKey(o => o.UserID)
               .OnDelete(DeleteBehavior.NoAction);


        builder.HasMany(o => o.SellerOrders)
               .WithOne(so => so.Order)
               .HasForeignKey(so => so.OrderId)
               .OnDelete(DeleteBehavior.Cascade);
        #endregion
    }

}
