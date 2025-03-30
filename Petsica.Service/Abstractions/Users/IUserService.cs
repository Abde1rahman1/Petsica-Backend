using Petsica.Shared.Contracts.Users.Request;
using Petsica.Shared.Contracts.Users.Response;

namespace Petsica.Service.Abstractions.Users
{
    public interface IUserService
    {
        Task<Result<UserProfileResponse>> GetProfileAsync(string userId);
        Task<Result> UpdateProfileAsync(string userId, UpdateProfileRequest request);
        Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request);
        Task<Result> AddService(string userId, ServiceRequest serviceRequest, CancellationToken cancellationToken);
        Task<Result<IEnumerable<AddSitterServiceResponse>>> GetServicesAsync(CancellationToken cancellationToken);
        Task<Result> ChooesServiceAsync(string userID, ChooesServiceRequest request, CancellationToken cancellationToken);
        Task<Result<IEnumerable<AddSitterServiceResponse>>> GetAllSitterService(string userID, CancellationToken cancellationToken);
        Task<Result> UpdateSitterService(string userID, UpdateSitterServiceRequest request, CancellationToken cancellationToken);
        Task<Result<IEnumerable<UserApprovalResponse>>> GetSellerApproval(string userId, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<UserApprovalResponse>>> GetSitterApproval(string userId, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<UserApprovalResponse>>> GetClinicApproval(string userId, CancellationToken cancellationToken = default);

        Task<Result> ApprovalUser(string userId, CancellationToken cancellationToken = default);

    }
}
