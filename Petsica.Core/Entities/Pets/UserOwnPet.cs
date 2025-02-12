﻿

namespace Petsica.Core.Entities.Pets;
public class UserOwnPet
{
    public int PetID { get; set; }
    public string UserID { get; set; }

    #region Navigation Properties
    public Pet Pet { get; set; }
    public User User { get; set; }
    #endregion
}
