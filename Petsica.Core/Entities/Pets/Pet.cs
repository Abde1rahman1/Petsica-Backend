
namespace Petsica.Core.Entities.Pets;
public class Pet
{
    public int PetID { get; set; }
    public string Species { get; set; }
    public string Photo { get; set; }
    public string Gender { get; set; }
    public string Name { get; set; }
    public string Breed { get; set; }
    public bool IsDelete { get; set; } = false;
    public bool Mating { get; set; } = false;
    public bool Adoption { get; set; } = false;



    #region Foreign Key
    public string UserID { get; set; }
    #endregion

    #region Navigation Property

    public User User { get; set; }
    #endregion
}
