

namespace Petsica.Core.Entities.Services;
public class Service
{
	public int ServiceID { get; set; }
	public int SitterID { get; set; }
	public decimal Price { get; set; }
	public string Description { get; set; }
	public string Title { get; set; }
	public string Location { get; set; }

	#region Navigation Property
	public User Sitter { get; set; }
	public required List<UserRequestService> Requests { get; set; }
	#endregion
}
