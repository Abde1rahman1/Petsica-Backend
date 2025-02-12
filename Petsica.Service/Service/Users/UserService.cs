using Petsica.Service.Abstractions.Users;
using Petsica.Shared.Contracts.Users.Request;
using Petsica.Shared.Contracts.Users.Response;

namespace Petsica.Service.Service.Users
{
    public class UserService(UserManager<ApplicationUser> userManager) : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;

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
    }


}
