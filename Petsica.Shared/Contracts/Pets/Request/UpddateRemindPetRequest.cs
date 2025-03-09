namespace Petsica.Shared.Contracts.Pets.Request
{
    public record UpdateRemindPetRequest(
   int PetId,
     string Title,
     string Description,
     DateTime Date
    );
}