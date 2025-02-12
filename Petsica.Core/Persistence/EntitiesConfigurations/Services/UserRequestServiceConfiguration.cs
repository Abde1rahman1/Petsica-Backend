

namespace Petsica.Core.Persistence.EntitiesConfigurations.ServicesS;
public class UserRequestServiceConfiguration : IEntityTypeConfiguration<UserRequestService>
{
    public void Configure(EntityTypeBuilder<UserRequestService> builder)
    {
        builder.HasKey(r => new { r.ServiceID, r.UserID });

        #region Relationships
        builder.HasOne(r => r.User)
               .WithMany(u => u.RequestedServices)
               .HasForeignKey(r => r.UserID).OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(r => r.Service)
               .WithMany(s => s.Requests)
               .HasForeignKey(r => r.ServiceID).OnDelete(DeleteBehavior.NoAction);
        #endregion
    }
}
