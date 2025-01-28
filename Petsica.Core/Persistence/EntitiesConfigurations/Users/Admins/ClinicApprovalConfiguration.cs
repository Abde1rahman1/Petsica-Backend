using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Core.Persistence.EntitiesConfigurations.Users.Admins;
public class ClinicApprovalConfiguration : IEntityTypeConfiguration<ClinicApproval>
{
    public void Configure(EntityTypeBuilder<ClinicApproval> builder)
    {
        builder.HasKey(c => c.ApprovalID);

        builder.Property(c => c.Status)
               .IsRequired()
               .HasMaxLength(10);

        #region Relationships
        builder.HasOne(c => c.Clinic)
               .WithMany(c => c.Approvals)
               .HasForeignKey(c => c.ClinicID);
        #endregion
    }
}
