using Petsica.Shared.Contracts.Users.Request;
using Petsica.Shared.Contracts.Users.Response;

namespace Petsica.Service.Abstractions.Users
{
    public interface IUserService
    {
        Task<Result<UserProfileResponse>> GetProfileAsync(string userId);
        Task<Result> UpdateProfileAsync(string userId, UpdateProfileRequest request);
        Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request);
        Task<Result> AddSitterService(string userId, ServiceRequest serviceRequest, CancellationToken cancellationToken);
        Task<Result<IEnumerable<AddSitterServiceResponse>>> GetServicesAsync(string userId, CancellationToken cancellationToken);
        Task<Result> ChooesServiceAsync(string userID, ChooesServiceRequest request, CancellationToken cancellationToken);
        Task<Result<IEnumerable<AddSitterServiceResponse>>> GetAllSitterService(string userID, CancellationToken cancellationToken);
        Task<Result> UpdateSitterService(string userID, UpdateSitterServiceRequest request, CancellationToken cancellationToken);
        Task<Result> DeleteSitterService(string userId, int serviceId, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<UserApprovallistResponse>>> GetSellerApproval(string userId, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<UserApprovallistResponse>>> GetSitterApproval(string userId, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<UserApprovallistResponse>>> GetClinicApproval(string userId, CancellationToken cancellationToken = default);
        Task<Result> ApprovalUser(ApprovalUserRequest request, CancellationToken cancellationToken = default);
        Task<Result<List<AllUsersResponse>>> GetAllUsers(string userId, CancellationToken cancellationToken = default);
        Task<Result> SetAdmin(SetAdminEmailRequest request, CancellationToken cancellationToken = default);
        Task<Result<UserApprovalResponse>> UserRequsestsDetails(ApprovalUserRequest request, CancellationToken cancellationToken = default);
        Task<Result<List<AllClinicsResponse>>> GetAllClinics(string userId, CancellationToken cancellationToken = default);
        Task<Result<ClinicApprovalResponse>> ClinicRequsestsDetails(ApprovalUserRequest request, CancellationToken cancellationToken = default);
    }
}
