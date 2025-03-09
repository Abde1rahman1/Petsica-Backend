namespace Petsica.Shared.Contracts.Pets.Request
{
    public record AddRemindPetRequest(
     int PetId,
     string Title,
     string Description,
     DateTime Date
        );
}