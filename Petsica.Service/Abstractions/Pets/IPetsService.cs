using Petsica.Shared.Contracts.Pets.Request;
using Petsica.Shared.Contracts.Pets.Response;

namespace Petsica.Service.Abstractions.Pets
{
    public interface IPetsService
    {
        Task<Result> AddPetAsync(string userId, AddPetRequest request, CancellationToken cancellationToken);
        Task<Result> AddRemindPetAsync(string userId, AddRemindPetRequest request, CancellationToken cancellationToken);
        Task<Result> UpdateRemindPetAsync(int RemindID, UpdateRemindPetRequest request, CancellationToken cancellationToken);
        Task<Result<IEnumerable<RemindPetResponse>>> GetAllRemindAsync(string userId, int PetID, CancellationToken cancellationToken);
        Task<Result<IEnumerable<PetsResponse>>> GetAllPetsAsync(string userId, CancellationToken cancellationToken);

        Task<Result> PetAdoptionOn(string userId, int petId, CancellationToken cancellationToken);
        Task<Result> PetMatingOn(string userId, int petId, CancellationToken cancellationToken);
        Task<Result> PetAdoptionOff(string userId, int petId, CancellationToken cancellationToken);
        Task<Result> PetMatingOff(string userId, int petId, CancellationToken cancellationToken);



        Task<Result<PetsServiceResponse>> GetPetServices(string userId, int PetID, CancellationToken cancellationToken);



    }
}
