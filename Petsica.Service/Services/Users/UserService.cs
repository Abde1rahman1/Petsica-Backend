using Hangfire;
using Petsica.Core.Const;
using Petsica.Core.Entities.Services;
using Petsica.Service.Abstractions.Users;
using Petsica.Shared.Const;
using Petsica.Shared.Contracts.Users.Request;
using Petsica.Shared.Contracts.Users.Response;
using Petsica.Shared.Email.Helpers;

namespace Petsica.Service.Services.Users
{
    public class UserService(UserManager<ApplicationUser> userManager,
        ApplicationDbContext context,
         IEmailSender emailSender,
     IHttpContextAccessor httpContextAccessor) : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ApplicationDbContext _context = context;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

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

        public async Task<Result<IEnumerable<AddSitterServiceResponse>>> GetServicesAsync(CancellationToken cancellationToken)
        {
            var result = await _context.Services.ToListAsync(cancellationToken);

            //var serviceResponses = result.Adapt<List<ServiceResponse>>();

            var serviceResponses = result.Select(service => new AddSitterServiceResponse(
                    service.ServiceID,
                    service.SitterID,
                    service.Price,
                    service.Description,
                    service.Title,
                    service.Location
                  )).ToList();

            return Result.Success<IEnumerable<AddSitterServiceResponse>>(serviceResponses);


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

        public async Task<Result<IEnumerable<AddSitterServiceResponse>>> GetAllSitterService(string userID, CancellationToken cancellationToken)
        {

            var serviceResponses = await _context.Services
                .Where(x => x.SitterID == userID)
                .ProjectToType<AddSitterServiceResponse>()
                .ToListAsync(cancellationToken);

            if (serviceResponses is null)
                return Result.Failure<IEnumerable<AddSitterServiceResponse>>(UserErrors.NoServicesYet);

            return Result.Success<IEnumerable<AddSitterServiceResponse>>(serviceResponses);
        }

        public async Task<Result> UpdateSitterService(string userID, UpdateSitterServiceRequest request, CancellationToken cancellationToken)
        {
            var service = await _context.Services
                .Where(x => x.ServiceID == request.ServiceID && x.SitterID == userID)
                .FirstOrDefaultAsync(cancellationToken);

            if (service is null)
                return Result.Failure(UserErrors.ServiceNotFound);


            service.Title = request.Title;
            service.Description = request.Description;
            service.Location = request.Location;
            service.Price = request.Price;

            try
            {
                await _context.Services.Where(x => x.ServiceID == request.ServiceID).
                     ExecuteUpdateAsync(service =>
                        service.
                        SetProperty(x => x.Title, request.Title)
                        .SetProperty(x => x.Description, request.Description)
                        .SetProperty(x => x.Location, request.Location)
                        .SetProperty(x => x.Price, request.Price), cancellationToken

                     );
                return Result.Success();

            }
            catch
            {
                return Result.Failure(UserErrors.ServiceNotFound);
            }

        }



        public async Task<Result<IEnumerable<UserApprovallistResponse>>> GetSellerApproval(string userId, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null || !(user.Type == RoleName.Admin))
                return Result.Failure<IEnumerable<UserApprovallistResponse>>(UserErrors.ServiceNotFound);

            var setters = _userManager.Users
                .Where(x => x.Type == RoleName.Seller && x.IsApproval == false)
                .ProjectToType<UserApprovallistResponse>()
                .ToList();


            return Result.Success<IEnumerable<UserApprovallistResponse>>(setters);

        }
        public async Task<Result<IEnumerable<UserApprovallistResponse>>> GetSitterApproval(string userId, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null || !(user.Type == RoleName.Admin))
                return Result.Failure<IEnumerable<UserApprovallistResponse>>(UserErrors.ServiceNotFound);

            var sitters = _userManager.Users
                .Where(x => x.Type == RoleName.Sitter && x.IsApproval == false)
                .ProjectToType<UserApprovallistResponse>()
                .ToList();


            return Result.Success<IEnumerable<UserApprovallistResponse>>(sitters);

        }

        public async Task<Result<IEnumerable<UserApprovallistResponse>>> GetClinicApproval(string userId, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null || !(user.Type == RoleName.Admin))
                return Result.Failure<IEnumerable<UserApprovallistResponse>>(UserErrors.ServiceNotFound);

            var clinics = _userManager.Users
                .Where(x => x.Type == RoleName.Clinic && x.IsApproval == false)
                .ProjectToType<UserApprovallistResponse>()
                .ToList();


            return Result.Success<IEnumerable<UserApprovallistResponse>>(clinics);

        }


        public async Task<Result> ApprovalUser(ApprovalUserRequest request, CancellationToken cancellationToken = default)
        {

            var user = await _userManager.FindByIdAsync(request.Userid);

            if (user is null)
                return Result.Failure(UserErrors.UserNotFound);

            //if (user.EmailConfirmed == false)
            //    return Result.Failure(UserErrors.EmailNotConfirmed);

            user.IsApproval = true;

            await _userManager.Users.Where(x => x.Id == request.Userid).
                     ExecuteUpdateAsync(service =>
                        service.
                        SetProperty(x => x.IsApproval, true), cancellationToken
                     );

            await SendApprovalEmail(user);


            return Result.Success();

        }


        public async Task<Result<List<AllUsersResponse>>> GetAllUsers(string userId, CancellationToken cancellationToken = default)
        {
            var users = await _userManager.Users
                              .Select(u => new AllUsersResponse(u.Id, u.UserName, u.Photo))
                              .ToListAsync();



            return Result<List<AllUsersResponse>>.Success(users);
        }

        public async Task<Result> SetAdmin(SetAdminEmailRequest request, CancellationToken cancellationToken = default)
        {
            if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
                return Result.Failure(UserErrors.ExistNotEmail);

            if (!user.EmailConfirmed)
                return Result.Failure(UserErrors.EmailNotConfirmed);

            if (user.Type != RoleName.Member)
                return Result.Failure(UserErrors.InvalidType);

            var result = await _userManager.AddToRoleAsync(user, DefaultRoles.Admin.Name);

            if (result.Succeeded)
                return Result.Success();

            return Result.Failure(UserErrors.DisabledUser);
        }


        public async Task<Result<UserApprovalResponse>> UserRequsestsDetails(ApprovalUserRequest request, CancellationToken cancellationToken = default)
        {

            var user = await _userManager.FindByIdAsync(request.Userid);

            if (user is null)
                return Result.Failure<UserApprovalResponse>(UserErrors.UserNotFound);

            if (user.Type == RoleName.Admin || user.Type == RoleName.Clinic)
                return Result.Failure<UserApprovalResponse>(UserErrors.InvalidType);



            var result = user.Adapt<UserApprovalResponse>();

            return Result.Success(result);
        }

        public async Task<Result<ClinicApprovalResponse>> ClinicRequsestsDetails(ApprovalUserRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(request.Userid);

            if (user is null)
                return Result.Failure<ClinicApprovalResponse>(UserErrors.UserNotFound);

            if (user.Type != RoleName.Clinic)
                return Result.Failure<ClinicApprovalResponse>(UserErrors.InvalidType);

            var result = user.Adapt<ClinicApprovalResponse>();

            return Result.Success(result);
        }


        private async Task SendApprovalEmail(ApplicationUser user)
        {
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

            var emailBody = EmailBodyBuilder.GenerateEmailBody("Approved",
                templateModel: new Dictionary<string, string>
                {
                { "{{name}}", user.UserName! },
                }
            );

            BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "✅ Petsica: Approved", emailBody));

            await Task.CompletedTask;
        }

    }


}
