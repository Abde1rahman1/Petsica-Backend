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

        //public async Task<Result> AddRemindPetAsync(string userId, AddRemindPetRequest request, CancellationToken cancellationToken)
        //{
        //    var result = request.Adapt<UserRemindPet>();

        //    result.UserID = userId;

        //    try
        //    {
        //        await _context.UserRemindPets.AddAsync(result, cancellationToken);
        //        await _context.SaveChangesAsync(cancellationToken);
        //    }
        //    catch
        //    {

        //        return Result.Failure(PetErrors.InvalidCreatePet);
        //    }

        //    return Result.Success();

        //}


        //public async Task<Result> UpdateRemindPetAsync(int RemindID, UpdateRemindPetRequest request, CancellationToken cancellationToken)
        //{


        //    var isExisting = await _context.UserRemindPets.AnyAsync(x => x.UserRemindPetID == RemindID, cancellationToken: cancellationToken);

        //    if (!isExisting)
        //        return Result.Failure(PetErrors.InvalidUpdateRemindPets);

        //    var currentRemind = await _context.UserRemindPets.FindAsync(RemindID, cancellationToken);

        //    if (currentRemind is null)
        //        return Result.Failure(PetErrors.RemindNotFound);

        //    currentRemind!.Description = request.Description;
        //    currentRemind.Date = request.Date;



        //    await _context.SaveChangesAsync(cancellationToken);

        //    return Result.Success();

        //}

        //public async Task<Result<IEnumerable<RemindPetResponse>>> GetAllRemindAsync(string userId, int PetID, CancellationToken cancellationToken)
        //{
        //    var petIsExists = await _context.Pets.AnyAsync(x => x.PetID == PetID, cancellationToken: cancellationToken);

        //    if (!petIsExists)
        //        return Result.Failure<IEnumerable<RemindPetResponse>>(PetErrors.PetNotFound);

        //    var response = await _context.UserRemindPets.
        //        Where(x => x.UserID == userId && x.PetID == PetID)
        //        .ProjectToType<RemindPetResponse>()
        //        .ToListAsync(cancellationToken);

        //    return Result.Success<IEnumerable<RemindPetResponse>>(response);

        //}

        public async Task<Result<IEnumerable<Pet>>> GetAllPetsAsync(string userId, CancellationToken cancellationToken)
        {
            var response = await _context.Pets.
                Where(x => x.UserID == userId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);



            return Result.Success<IEnumerable<Pet>>(response);

        }





        public async Task<Result> PetAdoptionOn(string userId, int petId, CancellationToken cancellationToken)
        {
            var petIsExists = await _context.Pets.AnyAsync(x => x.PetID == petId, cancellationToken: cancellationToken);

            if (!petIsExists)
                return Result.Failure(PetErrors.PetNotFound);

            var newService = new UserRequestPet
            {
                UserID = userId,
                PetID = petId,
                IsActive = true,
                Adoption = true,
                Mating = false,
            };

            var successService = Createservices(newService);

            return successService ? Result.Success() : Result.Failure(PetErrors.PetNotFound);
        }

        public async Task<Result> PetMatingOn(string userId, int petId, CancellationToken cancellationToken)
        {
            var petIsExists = await _context.Pets.AnyAsync(x => x.PetID == petId, cancellationToken: cancellationToken);

            if (!petIsExists)
                return Result.Failure(PetErrors.PetNotFound);


            var newService = new UserRequestPet
            {
                UserID = userId,
                PetID = petId,
                IsActive = true,
                Mating = true,
                Adoption = false,
            };

            var successService = Createservices(newService);


            return successService ? Result.Success() : Result.Failure(PetErrors.PetNotFound);

        }


        public async Task<Result> PetAdoptionOff(string userId, int petId, CancellationToken cancellationToken)
        {
            var petIsExists = await _context.Pets.AnyAsync(x => x.PetID == petId, cancellationToken: cancellationToken);

            if (!petIsExists)
                return Result.Failure(PetErrors.PetNotFound);

            var newService = new UserRequestPet
            {
                UserID = userId,
                PetID = petId,
                IsActive = false,
                Adoption = false,
                Mating = false,
            };

            var successService = Createservices(newService);

            return successService ? Result.Success() : Result.Failure(PetErrors.PetNotFound);
        }

        public async Task<Result> PetMatingOff(string userId, int petId, CancellationToken cancellationToken)
        {
            var petIsExists = await _context.Pets.AnyAsync(x => x.PetID == petId, cancellationToken: cancellationToken);

            if (!petIsExists)
                return Result.Failure(PetErrors.PetNotFound);


            var newService = new UserRequestPet
            {
                UserID = userId,
                PetID = petId,
                IsActive = false,
                Mating = false,
                Adoption = false,
            };

            var successService = Createservices(newService);


            return successService ? Result.Success() : Result.Failure(PetErrors.PetNotFound);

        }

        public async Task<Result<PetsServiceResponse>> GetPetProfil(string userId, int PetID, CancellationToken cancellationToken)
        {
            var petIsExists = await _context.Pets.AnyAsync(x => x.PetID == PetID, cancellationToken: cancellationToken);

            if (!petIsExists)
                return Result.Failure<PetsServiceResponse>(PetErrors.PetNotFound);

            var response = await _context.UserRequestPets
                .Where(x => x.UserID == userId && x.PetID == PetID)
                .Include(p => p.Pet)
                .ProjectToType<PetsServiceResponse>()
                .AsNoTracking()
                .SingleOrDefaultAsync(cancellationToken); ;

            if (response is null)
                return Result.Failure<PetsServiceResponse>(PetErrors.PetNotFound);

            return Result.Success(response);
        }

        public async Task<Result<IEnumerable<PetsMatingResponse>>> GetPetMatingList(CancellationToken CancellationToken)
        {
            var matingList = await _context.UserRequestPets
                .Where(x => x.Mating == true)
                .Include(p => p.Pet)
                .ProjectToType<PetsMatingResponse>()
                .ToListAsync(cancellationToken: CancellationToken);

            if (matingList is null)
                return Result.Failure<IEnumerable<PetsMatingResponse>>(PetErrors.PetNotFound);

            return Result.Success<IEnumerable<PetsMatingResponse>>(matingList);


        }
        public async Task<Result<IEnumerable<PetsAdoptionResponse>>> GetPetAdoptionList(CancellationToken CancellationToken)
        {
            var AdoptionList = await _context.UserRequestPets
                .Where(x => x.Adoption == true)
                .Include(p => p.Pet)
                .ProjectToType<PetsAdoptionResponse>()
                .ToListAsync(cancellationToken: CancellationToken);

            if (AdoptionList is null)
                return Result.Failure<IEnumerable<PetsAdoptionResponse>>(PetErrors.PetNotFound);

            return Result.Success<IEnumerable<PetsAdoptionResponse>>(AdoptionList);


        }

        public bool Createservices(UserRequestPet request)
        {
            var serviceIsExists = _context.UserRequestPets.Any(x => x.UserID == request.UserID && x.UserID == request.UserID);

            if (!serviceIsExists)
            {
                try
                {
                    _context.UserRequestPets.Add(request);
                    _context.SaveChanges();
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    _context.UserRequestPets.Update(request);
                    _context.SaveChanges();
                }
                catch
                {
                    return false;
                }
            }

            return true;

        }


    }
}
