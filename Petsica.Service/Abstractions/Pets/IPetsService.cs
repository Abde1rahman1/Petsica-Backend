using Petsica.Shared.Contracts.Pets.Request;
using Petsica.Shared.Contracts.Pets.Response;

namespace Petsica.Service.Abstractions.Pets
{
    public interface IPetsService
    {
        Task<Result> AddPetAsync(string userId, AddPetRequest request, CancellationToken cancellationToken);
        Task<Result<PetsProfilResponse>> GetPetProfil(string userId, int PetID, CancellationToken cancellationToken);
        Task<Result<IEnumerable<PetsResponse>>> GetAllPetsAsync(string userId, CancellationToken cancellationToken);
        Task<Result> PetMatingToggle(string userId, int petId, CancellationToken cancellationToken);
        Task<Result> PetAdoptionToggle(string userId, int petId, CancellationToken cancellationToken);
        Task<Result<IEnumerable<PetsMatingResponse>>> GetPetMatingList(CancellationToken CancellationToken);
        Task<Result<IEnumerable<PetsAdoptionResponse>>> GetPetAdoptionList(CancellationToken CancellationToken);



    }
}
