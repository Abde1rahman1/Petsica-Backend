
namespace Petsica.Core.Entities.Users.AdminEntity;
public class ClinicApproval
{
	public int ApprovalID { get; set; }
	public string Status { get; set; }
	public DateTime ApprovalDate { get; set; }

	#region Foreign Keys
	public int AdminID { get; set; }
	public int ClinicID { get; set; }
	#endregion

	#region Navigation Properties
	public Admin Admin { get; set; }
	public Clinic Clinic { get; set; }
	#endregion
}
