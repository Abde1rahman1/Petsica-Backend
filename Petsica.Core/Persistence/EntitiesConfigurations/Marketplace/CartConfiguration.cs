namespace Petsica.Core.Persistence.EntitiesConfigurations.Marketplace
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(c => c.Id);

            #region Relationships
            builder.HasOne(c => c.User)
                   .WithMany()
                   .HasForeignKey(c => c.UserID)
                   .OnDelete(DeleteBehavior.NoAction);
            #endregion
        }
    }
}
