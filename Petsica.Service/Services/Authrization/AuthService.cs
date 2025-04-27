using Hangfire;
using Petsica.Core.Const;
using Petsica.Shared.Const;
using Petsica.Shared.Email.Helpers;

namespace Petsica.Service.Services.Authrization
{
    public class AuthService(
     UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
     IJwtProvider jwtProvider,
     ILogger<AuthService> logger,
     ApplicationDbContext context,
     IEmailSender emailSender,
     IHttpContextAccessor httpContextAccessor
    ) : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly ILogger<AuthService> _logger = logger;
        private readonly ApplicationDbContext _context = context;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly int _refreshTokenExpiryDays = 14;

        public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            if (await _userManager.FindByEmailAsync(email) is not { } user)
                return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

            if ((user.Type == RoleName.Seller || user.Type == RoleName.Sitter || user.Type == RoleName.Clinic) && !user.IsApproval)
                return Result.Failure<AuthResponse>(UserErrors.NotApproval);

            if (user.IsDisabled)
                return Result.Failure<AuthResponse>(UserErrors.DisabledUser);

            var result = await _signInManager.PasswordSignInAsync(user, password, false, true);

            if (result.Succeeded)
            {
                var (userRoles, userPermissions) = await GetUserRolesAndPermissions(user, cancellationToken);

                var (token, expiresIn) = _jwtProvider.GenerateToken(user, userRoles, userPermissions);
                var refreshToken = GenerateRefreshToken();
                var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

                user.RefreshTokens.Add(new RefreshToken
                {
                    Token = refreshToken,
                    ExpiresOn = refreshTokenExpiration
                });

                await _userManager.UpdateAsync(user);

                var response = new AuthResponse(user.Id, user.Email, token, expiresIn, refreshToken, refreshTokenExpiration);

                return Result.Success(response);
            }

            var error = result.IsNotAllowed
                ? UserErrors.EmailNotConfirmed
                : result.IsLockedOut
                ? UserErrors.LockedUser
                : UserErrors.InvalidCredentials;

            return Result.Failure<AuthResponse>(error);
        }


        public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId = _jwtProvider.ValidateToken(token);

            if (userId is null)
                return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);

            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);

            if (userRefreshToken is null)
                return Result.Failure<AuthResponse>(UserErrors.InvalidRefreshToken);

            userRefreshToken.RevokedOn = DateTime.UtcNow;

            var (userRoles, userPermissions) = await GetUserRolesAndPermissions(user, cancellationToken);

            var (newToken, expiresIn) = _jwtProvider.GenerateToken(user, userRoles, userPermissions);
            var newRefreshToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                ExpiresOn = refreshTokenExpiration
            });

            await _userManager.UpdateAsync(user);

            var response = new AuthResponse(user.Id, user.Email, newToken, expiresIn, newRefreshToken, refreshTokenExpiration);

            return Result.Success(response);
        }

        public async Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId = _jwtProvider.ValidateToken(token);

            if (userId is null)
                return Result.Failure(UserErrors.InvalidJwtToken);

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return Result.Failure(UserErrors.InvalidJwtToken);

            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);

            if (userRefreshToken is null)
                return Result.Failure(UserErrors.InvalidRefreshToken);

            userRefreshToken.RevokedOn = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);

            return Result.Success();
        }

        public async Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            if (request.Type == RoleName.Admin || request.Type == RoleName.Clinic)
                return Result.Failure(UserErrors.InvalidType);

            var emailIsExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);

            if (emailIsExists)
                return Result.Failure(UserErrors.DuplicatedEmail);

            var user = request.Adapt<ApplicationUser>();

            var result = await _userManager.CreateAsync(user, request.Password);

            var userContext = new User
            {
                UserID = user.Id
            };


            await _context.Users.AddAsync(userContext, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");


                _logger.LogInformation("Confirmation code: {code}", code);

                await SendConfirmationEmail(user, code);

                return Result.Success();
            }

            var error = result.Errors.First();

            return Result.Failure(new Errors(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        public async Task<Result> ClinicRegisterAsync(ClinicRegisterRequest request, CancellationToken cancellationToken = default)
        {
            if (request.Type != RoleName.Clinic)
                return Result.Failure(UserErrors.InvalidType);

            var emailIsExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);

            if (emailIsExists)
                return Result.Failure(UserErrors.DuplicatedEmail);

            var user = request.Adapt<ApplicationUser>();

            user.Type = RoleName.Clinic;

            var result = await _userManager.CreateAsync(user, request.Password);


            var clinic = new Clinic
            {
                ClinicID = user.Id,
                WorkingHours = request.WorkingHours,
                ContactInfo = request.ContactInfo,
            };

            await _context.Clinics.AddAsync(clinic, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            if (result.Succeeded)
            {
                var code = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

                _logger.LogInformation("Confirmation code: {code}", code);

                await SendConfirmationEmail(user, code);
                return Result.Success();
            }

            var error = result.Errors.First();

            return Result.Failure(new Errors(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }



        public async Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
                return Result.Failure(UserErrors.InvalidCode);

            if (user.EmailConfirmed)
                return Result.Failure(UserErrors.DuplicatedConfirmation);

            var code = request.Code;

            var result = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", code);

            if (result == true)
            {

                user.EmailConfirmed = true;

                if (user.Type == RoleName.Member)
                    await _userManager.AddToRoleAsync(user, DefaultRoles.Member.Name);
                else if (user.Type == RoleName.Seller)
                    await _userManager.AddToRoleAsync(user, DefaultRoles.Seller.Name);
                else if (user.Type == RoleName.Sitter)
                    await _userManager.AddToRoleAsync(user, DefaultRoles.Sitter.Name);
                else if (user.Type == RoleName.Clinic)
                    await _userManager.AddToRoleAsync(user, DefaultRoles.Clinic.Name);


                return Result.Success();
            }

            return Result.Failure(UserErrors.InvalidCode);
        }

        public async Task<Result> ResendConfirmationEmailAsync(ResendConfirmationEmailRequest request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
                return Result.Success();

            if (user.EmailConfirmed)
                return Result.Failure(UserErrors.DuplicatedConfirmation);

            var code = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            _logger.LogInformation("Confirmation code: {code}", code);

            await SendConfirmationEmail(user, code);

            return Result.Success();
        }
        public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null || !user.EmailConfirmed)
                return Result.Failure(UserErrors.InvalidCode);


            IdentityResult result;

            try
            {
                var VerifyTwoFactor = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", request.Code);

                if (VerifyTwoFactor == false)
                {
                    return Result.Failure(UserErrors.InvalidCode);

                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);


                result = await _userManager.ResetPasswordAsync(user, code, request.NewPassword);
            }
            catch (FormatException)
            {
                result = IdentityResult.Failed(_userManager.ErrorDescriber.InvalidToken());
            }

            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();

            return Result.Failure(new Errors(error.Code, error.Description, StatusCodes.Status401Unauthorized));
        }

        public async Task<Result> SendResetPasswordCodeAsync(string email)
        {
            if (await _userManager.FindByEmailAsync(email) is not { } user)
                return Result.Success();

            if (!user.EmailConfirmed)
                return Result.Failure(UserErrors.EmailNotConfirmed);

            var code = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");


            _logger.LogInformation("Reset code: {code}", code);

            await SendResetPasswordEmail(user, code);

            return Result.Success();
        }
        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }


        private async Task<(IEnumerable<string> roles, IEnumerable<string> permissions)> GetUserRolesAndPermissions(ApplicationUser user, CancellationToken cancellationToken)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            //var userPermissions = await _context.Roles
            //    .Join(_context.RoleClaims,
            //        role => role.Id,
            //        claim => claim.RoleId,
            //        (role, claim) => new { role, claim }
            //    )
            //    .Where(x => userRoles.Contains(x.role.Name!))
            //    .Select(x => x.claim.ClaimValue!)
            //    .Distinct()
            //    .ToListAsync(cancellationToken);

            var userPermissions = await (from r in _context.Roles
                                         join p in _context.RoleClaims
                                         on r.Id equals p.RoleId
                                         where userRoles.Contains(r.Name!)
                                         select p.ClaimValue!)
                                         .Distinct()
                                         .ToListAsync(cancellationToken);

            return (userRoles, userPermissions);
        }


        private async Task SendResetPasswordEmail(ApplicationUser user, string code)
        {
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

            var emailBody = EmailBodyBuilder.GenerateEmailBody("ForgetPassword",
                templateModel: new Dictionary<string, string>
                {
                { "{{name}}", user.UserName! },
                { "{{Code}}",code}
                }
            );

            BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "✅ Petsica: Change Password", emailBody));

            await Task.CompletedTask;
        }

        private async Task SendConfirmationEmail(ApplicationUser user, string code)
        {
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

            var emailBody = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation",
                templateModel: new Dictionary<string, string>
                {
                { "{{name}}", user.UserName! },
                    { "{{Code}}",code }
                }
            );

            BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "✅ Petsica: Email Confirmation", emailBody));

            await Task.CompletedTask;
        }
    }
}
