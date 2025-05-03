using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Core.Persistence.EntitiesConfigurations.Marketplace
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.Price)
                   .HasPrecision(18, 2);

            builder.Property(oi => oi.Discount)
                   .HasPrecision(18, 2);

            builder.Property(oi => oi.Quantity)
                   .IsRequired();

            #region Relationships
            builder.HasOne(oi => oi.SellerOrder)
                   .WithMany(so => so.OrderItems)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(oi => oi.Product)
                   .WithMany(p => p.OrderItems)
                   .HasForeignKey(oi => oi.ProductId)
                   .OnDelete(DeleteBehavior.NoAction);
            #endregion
        }
    }
}
