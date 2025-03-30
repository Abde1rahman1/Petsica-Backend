namespace Petsica.Shared.Contracts.Pets.Response
{
    public record PetsResponse(
    int PetID,
    string Species,
    string Photo,
    string Gender,
    string Name,
    string Breed
        );
}