namespace Petsica.Shared.Contracts.Pets.Response
{
    public record RemindPetResponse(
     int UserRemindPetID,
     string Title,
     string Description,
     DateTime Date
        );
}