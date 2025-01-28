

namespace Petsica.Core.Entities.Pets;
public class UserRequestPet
{
	public int PetID { get; set; }
	public int UserID { get; set; }
	public string Type { get; set; }

	#region Navigation Properties
	public Pet Pet { get; set; }
	public User User { get; set; }
	#endregion
}
