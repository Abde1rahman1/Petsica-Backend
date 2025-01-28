
namespace Petsica.Core.Entities.Users.AdminEntity;
public class Admin
{
	public int AdminID { get; set; }
	public string Password { get; set; }
	public string Email { get; set; }
	public string Username { get; set; }
	#region Navigation Properties
	public List<SitterApproval> SitterApprovals { get; set; }
	public List<SellerApproval> SellerApprovals { get; set; }
	public List<ClinicApproval> ClinicApprovals { get; set; }
	#endregion
}
