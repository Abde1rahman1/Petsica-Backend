using Microsoft.AspNetCore.Http;

namespace Petsica.Shared.Error
{
    public static class PetErrors
    {
        public static readonly Errors InvalidCreatePet =
            new("Pet.InvalidCreatePet", "Failed to create the PET.", StatusCodes.Status400BadRequest);

        public static readonly Errors InvalidUpdateRemindPets =
           new("Pet.InvalidUpdateRemindPets", "Failed to Update Remind Pets", StatusCodes.Status400BadRequest);

        public static readonly Errors RemindNotFound =
           new("Pet.NotFound", "No Pet was found with the given ID", StatusCodes.Status404NotFound);

        public static readonly Errors PetNotFound =
           new("Pet.NotFound", "No Pet was found with the given ID", StatusCodes.Status404NotFound);

        public static readonly Errors InvalidAdoptio =
            new("Pet.InvalidAdoptio", "Failed to Add the Adoption ", StatusCodes.Status400BadRequest);
    }
}
