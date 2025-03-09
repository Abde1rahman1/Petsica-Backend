
namespace Petsica.Core.Entities.Pets;
public class UserRemindPet
{

    public int UserRemindPetID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }


    public int PetID { get; set; }
    public string UserID { get; set; }
    #region Navigation Properties
    public Pet Pet { get; set; }
    public User User { get; set; }
    #endregion

}
