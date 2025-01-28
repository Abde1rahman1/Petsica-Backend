using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Core.Entities.Users;
public class Clinic
{
	public int ClinicID { get; set; }
	public string Photo { get; set; }
	public string PhoneNumber { get; set; }
	public string Address { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public string Username { get; set; }
	public string Location { get; set; }
	public string WorkingHours { get; set; }
	public int VerificationID { get; set; }
	public string ContactInfo { get; set; }

	#region Navigation Properties
	public List<ClinicApproval> Approvals { get; set; }
	public List<ClinicMessageClinic> ClinicMessages { get; set; }
	public List<UserMessageClinic> UserMessages { get; set; }
	#endregion
}
