
namespace Petsica.Core.Entities.Pets;
public class Pet
{
    public int PetID { get; set; }
    public string Species { get; set; }
    public string Photo { get; set; }
    public string Gender { get; set; }
    public string Name { get; set; }
    public string Breed { get; set; }


    #region Foreign Key
    public int UserID { get; set; }
    #endregion

    #region Navigation Property
  
    public User User { get; set; }
    #endregion
}
