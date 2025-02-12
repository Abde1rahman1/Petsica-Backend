

namespace Petsica.Core.Persistence.EntitiesConfigurations.Marketplace;
public class UserMakeOrderConfiguration : IEntityTypeConfiguration<UserMakeOrder>
{
    public void Configure(EntityTypeBuilder<UserMakeOrder> builder)
    {
        builder.HasKey(o => new { o.UserID, o.OrderID });

        #region Relationships
        builder.HasOne(o => o.User)
               .WithMany()
               .HasForeignKey(o => o.UserID)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(o => o.Order)
               .WithMany()
               .HasForeignKey(o => o.OrderID)
               .OnDelete(DeleteBehavior.NoAction);
        #endregion
    }
}
