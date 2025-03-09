using Petsica.Core.Entities.Services;
using Petsica.Service.Abstractions.Users;
using Petsica.Shared.Contracts.Users.Request;
using Petsica.Shared.Contracts.Users.Response;

namespace Petsica.Service.Services.Users
{
    public class UserService(UserManager<ApplicationUser> userManager, ApplicationDbContext context) : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ApplicationDbContext _context = context;

        public async Task<Result<UserProfileResponse>> GetProfileAsync(string userId)
        {
            var user = await _userManager.Users
                .Where(x => x.Id == userId)
                .ProjectToType<UserProfileResponse>()
                .SingleAsync();

            return Result.Success(user);
        }

        public async Task<Result> UpdateProfileAsync(string userId, UpdateProfileRequest request)
        {

            await _userManager.Users
                .Where(x => x.Id == userId)
                .ExecuteUpdateAsync(setters =>
                    setters
                        .SetProperty(x => x.UserName, request.UserName)
                        .SetProperty(x => x.Address, request.Address)
                        .SetProperty(x => x.Location, request.Location)
                        .SetProperty(x => x.Photo, request.Photo)
                );

            return Result.Success();
        }

        public async Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var result = await _userManager.ChangePasswordAsync(user!, request.CurrentPassword, request.NewPassword);

            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();

            return Result.Failure(new Errors(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        public async Task<Result> AddService(string userId, ServiceRequest serviceRequest, CancellationToken cancellationToken)
        {

            var request = new SitterService
            {
                SitterID = userId,
                Description = serviceRequest.Description,
                Title = serviceRequest.Title,
                Location = serviceRequest.Location,
                Price = serviceRequest.Price
            };

            try
            {
                await _context.Services.AddAsync(request, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                return Result.Failure(UserErrors.InvalidCreateService);
            }

            return Result.Success();
        }

        public async Task<Result<List<ServiceResponse>>> GetServicesAsync(CancellationToken cancellationToken)
        {
            var result = await _context.Services.ToListAsync(cancellationToken);

            //var serviceResponses = result.Adapt<List<ServiceResponse>>();

            var serviceResponses = result.Select(service => new ServiceResponse(
                    service.ServiceID,
                    service.SitterID,
                    service.Price,
                    service.Description,
                    service.Title,
                    service.Location
                  )).ToList();

            return Result.Success(serviceResponses);


        }

        public async Task<Result> ChooesServiceAsync(string userID, ChooesServiceRequest request, CancellationToken cancellationToken)
        {
            var requestService = new UserRequestService
            {
                UserID = userID,
                ServiceID = request.ServiceID,
            };

            try
            {
                await _context.UserRequestServices.AddAsync(requestService, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                return Result.Failure(UserErrors.InvalidChooesService);
            }

            return Result.Success();

        }
    }



}
