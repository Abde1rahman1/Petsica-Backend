
namespace Petsica.Core.Persistence.EntitiesConfigurations.Marketplace;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.OrderID);

        #region Relationships
        builder.HasOne(o => o.User)
               .WithMany(u => u.Orders)
               .HasForeignKey(o => o.UserID).OnDelete(DeleteBehavior.NoAction);
        #endregion
    }
}
