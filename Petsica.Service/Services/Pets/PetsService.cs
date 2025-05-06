using Petsica.Core.Entities.Pets;
using Petsica.Service.Abstractions.Pets;
using Petsica.Shared.Contracts.Pets.Request;
using Petsica.Shared.Contracts.Pets.Response;

namespace Petsica.Service.Services.Pets
{
    public class PetsService(ApplicationDbContext context) : IPetsService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Result> AddPetAsync(string userId, AddPetRequest request, CancellationToken cancellationToken)
        {
            var result = request.Adapt<Pet>();

            result.UserID = userId;

            try
            {
                await _context.Pets.AddAsync(result, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch
            {

                return Result.Failure(PetErrors.InvalidCreatePet);
            }

            return Result.Success();

        }


        public async Task<Result<IEnumerable<PetsResponse>>> GetAllPetsAsync(string userId, CancellationToken cancellationToken)
        {
            var response = await _context.Pets.
                Where(x => x.UserID == userId && !x.IsDelete)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var result = response.Select(pets => new PetsResponse
            (
                PetID: pets.PetID,
                Species: pets.Species,
                Photo: pets.Photo,
                Gender: pets.Gender,
                Name: pets.Name,
                Breed: pets.Breed
                )
                ).ToList();

            return Result.Success<IEnumerable<PetsResponse>>(result);

        }



        public async Task<Result> PetAdoptionToggle(string userId, int petId, CancellationToken cancellationToken)
        {
            var pet = await _context.Pets.FirstAsync(x => x.PetID == petId, cancellationToken: cancellationToken);

            if (pet is null)
                return Result.Failure(PetErrors.PetNotFound);

            if (pet.IsDelete)
                return Result.Failure<PetsProfilResponse>(PetErrors.PetNotFound);

            pet.Adoption = !pet.Adoption;

            var successService = Createservices(pet);

            return successService ? Result.Success() : Result.Failure(PetErrors.PetNotFound);
        }

        public async Task<Result> PetMatingToggle(string userId, int petId, CancellationToken cancellationToken)
        {
            var pet = await _context.Pets.FirstAsync(x => x.PetID == petId, cancellationToken: cancellationToken);

            if (pet is null)
                return Result.Failure(PetErrors.PetNotFound);
            if (pet.IsDelete)
                return Result.Failure<PetsProfilResponse>(PetErrors.PetNotFound);

            pet.Mating = !pet.Mating;

            var successService = Createservices(pet);

            return successService ? Result.Success() : Result.Failure(PetErrors.PetNotFound);

        }


        public async Task<Result<PetsProfilResponse>> GetPetProfil(string userId, int PetID, CancellationToken cancellationToken)
        {

            var response = await _context.Pets
                .FirstAsync(x => x.UserID == userId && x.PetID == PetID, cancellationToken);

            if (response is null)
                return Result.Failure<PetsProfilResponse>(PetErrors.PetNotFound);

            if (response.IsDelete)
                return Result.Failure<PetsProfilResponse>(PetErrors.PetNotFound);

            var result = new PetsProfilResponse
                (
                response!.PetID,
                response.Species,
                response.Photo,
                response.Gender,
                response.Name,
                response.Breed,
                response.IsDelete,
                response.Mating,
                response.Adoption
                );


            return Result.Success(result);
        }

        public async Task<Result<IEnumerable<PetsMatingResponse>>> GetPetMatingList(CancellationToken CancellationToken)
        {
            var matingList = await _context.Pets
                .Where(x => x.Mating && !x.IsDelete)
                .ToListAsync(cancellationToken: CancellationToken);


            if (matingList is null)
                return Result.Failure<IEnumerable<PetsMatingResponse>>(PetErrors.PetNotFound);

            var result = matingList.Select(pets => new PetsMatingResponse
                (
                  PetID: pets.PetID,
                  UserName: _context.Users.First(x => x.Id == pets.UserID).UserName!,
                  Species: pets.Species,
                  Photo: pets.Photo,
                  Gender: pets.Gender,
                  Name: pets.Name,
                  Breed: pets.Breed
                )
                 ).ToList();

            return Result.Success<IEnumerable<PetsMatingResponse>>(result);


        }
        public async Task<Result<IEnumerable<PetsAdoptionResponse>>> GetPetAdoptionList(CancellationToken CancellationToken)
        {
            var AdoptionList = await _context.Pets
                .Where(x => x.Adoption && !x.IsDelete)
                .ToListAsync(cancellationToken: CancellationToken);

            if (AdoptionList is null)
                return Result.Failure<IEnumerable<PetsAdoptionResponse>>(PetErrors.PetNotFound);

            var result = AdoptionList.Select(pets => new PetsAdoptionResponse
              (
                  PetID: pets.PetID,
                  UserName: _context.Users.First(x => x.Id == pets.UserID).UserName!,
                  Species: pets.Species,
                  Photo: pets.Photo,
                  Gender: pets.Gender,
                  Name: pets.Name,
                  Breed: pets.Breed
                )
              ).ToList();

            return Result.Success<IEnumerable<PetsAdoptionResponse>>(result);


        }

        public async Task<Result> DeletePet(string userId, int petId, CancellationToken cancellationToken = default)
        {


            try
            {
                await _context.Pets
                  .Where(p => p.UserID == userId && p.PetID == petId)
                  .ExecuteUpdateAsync(pet =>
                  pet.SetProperty(p => p.IsDelete, true), cancellationToken
                    );
            }
            catch
            {
                return Result.Failure(PetErrors.PetNotFound);
            }

            return Result.Success();
        }


        public async Task<Result> UpdatePet(string userId, UpdatePetRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.Pets
                  .Where(p => p.UserID == userId && p.PetID == request.PetId)
                  .ExecuteUpdateAsync(pet =>
                  pet.SetProperty(p => p.Name, request.Name)
                  .SetProperty(p => p.Species, request.Species)
                  .SetProperty(p => p.Photo, request.Photo)
                  .SetProperty(p => p.Gender, request.Gender)
                  .SetProperty(p => p.Breed, request.Breed), cancellationToken
                    );
            }
            catch
            {
                return Result.Failure(PetErrors.PetNotFound);
            }

            return Result.Success();

        }
        public bool Createservices(Pet request)
        {
            try
            {
                _context.Pets.Update(request);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;

        }


    }
}
