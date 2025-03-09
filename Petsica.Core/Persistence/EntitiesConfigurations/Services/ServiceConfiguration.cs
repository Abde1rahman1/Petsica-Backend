

namespace Petsica.Core.Persistence.EntitiesConfigurations.Services;
public class ServiceConfiguration : IEntityTypeConfiguration<SitterService>
{
    public void Configure(EntityTypeBuilder<SitterService> builder)
    {
        builder.HasKey(s => s.ServiceID);


        builder.Property(s => s.Title)
               .IsRequired()
               .HasMaxLength(25);

        builder.Property(s => s.Price)
        .HasColumnType("decimal(18, 2)");

        #region Relationships
        builder.HasOne(s => s.Sitter)
               .WithMany()
               .HasForeignKey(s => s.SitterID).OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(s => s.Requests)
               .WithOne(r => r.Service)
               .HasForeignKey(r => r.ServiceID).OnDelete(DeleteBehavior.NoAction);
        #endregion
    }
}

