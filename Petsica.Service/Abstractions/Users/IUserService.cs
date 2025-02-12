using Petsica.Shared.Contracts.Users.Request;
using Petsica.Shared.Contracts.Users.Response;

namespace Petsica.Service.Abstractions.Users
{
    public interface IUserService
    {
        Task<Result<UserProfileResponse>> GetProfileAsync(string userId);
        Task<Result> UpdateProfileAsync(string userId, UpdateProfileRequest request);
        Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request);
    }
}
