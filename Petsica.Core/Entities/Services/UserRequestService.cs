
namespace Petsica.Core.Entities.Services;
public class UserRequestService
{
	public int ServiceID { get; set; }
	public int UserID { get; set; }

	#region Navigation Properties
	public Service Service { get; set; }
	public User User { get; set; }
	#endregion
}
