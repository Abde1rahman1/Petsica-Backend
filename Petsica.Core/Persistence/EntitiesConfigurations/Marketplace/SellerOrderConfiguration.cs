using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Core.Persistence.EntitiesConfigurations.Marketplace
{
    public class SellerOrderConfiguration : IEntityTypeConfiguration<SellerOrder>
    {
        public void Configure(EntityTypeBuilder<SellerOrder> builder)
        {
            builder.HasKey(so => so.SellerOrderId);

            builder.Property(so => so.TotalPrice).HasColumnType("decimal(18,2)");

            builder.HasOne(so => so.Seller)
                .WithMany()
                .HasForeignKey(so => so.SellerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(so => so.OrderItems)
                .WithOne(oi => oi.SellerOrder)
                .HasForeignKey(oi => oi.SellerOrderId);
        }
    }

}
