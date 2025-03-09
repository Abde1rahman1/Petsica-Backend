namespace Petsica.Shared.Contracts.Pets.Request
{
    public record AddPetRequest(
    string Species,
    string Photo,
    string Gender,
    string Name,
    string Breed
        );

}

