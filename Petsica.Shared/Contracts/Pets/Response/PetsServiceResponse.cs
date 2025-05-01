namespace Petsica.Shared.Contracts.Pets.Response
{
    public record PetsProfilResponse(

       int PetID,
       string Species,
       string Photo,
       string Gender,
       string Name,
       string Breed,
       bool IsActive,
       bool Mating,
       bool Adoption
    );


}