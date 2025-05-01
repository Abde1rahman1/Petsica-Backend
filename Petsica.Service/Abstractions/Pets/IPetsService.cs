using Petsica.Core.Entities.Pets;
using Petsica.Shared.Contracts.Pets.Request;
using Petsica.Shared.Contracts.Pets.Response;

namespace Petsica.Service.Abstractions.Pets
{
    public interface IPetsService
    {
        Task<Result> AddPetAsync(string userId, AddPetRequest request, CancellationToken cancellationToken);
        Task<Result<PetsServiceResponse>> GetPetProfil(string userId, int PetID, CancellationToken cancellationToken);
        Task<Result<IEnumerable<Pet>>> GetAllPetsAsync(string userId, CancellationToken cancellationToken);

        // Pet Service 
        Task<Result> PetAdoptionOn(string userId, int petId, CancellationToken cancellationToken);
        Task<Result> PetMatingOn(string userId, int petId, CancellationToken cancellationToken);
        Task<Result> PetAdoptionOff(string userId, int petId, CancellationToken cancellationToken);
        Task<Result> PetMatingOff(string userId, int petId, CancellationToken cancellationToken);
        Task<Result<IEnumerable<PetsMatingResponse>>> GetPetMatingList(CancellationToken CancellationToken);
        Task<Result<IEnumerable<PetsAdoptionResponse>>> GetPetAdoptionList(CancellationToken CancellationToken);



    }
}
