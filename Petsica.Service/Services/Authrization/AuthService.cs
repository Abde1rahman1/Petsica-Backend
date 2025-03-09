namespace Petsica.Service.Services.Authrization
{
    public class AuthService(
     UserManager<ApplicationUser> userManager,
     IJwtProvider jwtProvider,
     ILogger<AuthService> logger,
     ApplicationDbContext context
    ) : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly ILogger<AuthService> _logger = logger;
        private readonly ApplicationDbContext _context = context;
        private readonly int _refreshTokenExpiryDays = 14;

        public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
        {

            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

            var isValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!isValidPassword)
                return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials); ;

            var (token, expiresIn) = _jwtProvider.GenerateToken(user);
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

            var (newToken, expiresIn) = _jwtProvider.GenerateToken(user);
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
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                _logger.LogInformation("Confirmation code: {code}", code);


                return Result.Success();
            }

            var error = result.Errors.First();

            return Result.Failure(new Errors(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        public async Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request)
        {
            if (await _userManager.FindByIdAsync(request.UserId) is not { } user)
                return Result.Failure(UserErrors.InvalidCode);

            if (user.EmailConfirmed)
                return Result.Failure(UserErrors.DuplicatedConfirmation);

            var code = request.Code;

            try
            {
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            }
            catch (FormatException)
            {
                return Result.Failure(UserErrors.InvalidCode);
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();

            return Result.Failure(new Errors(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        public async Task<Result> ResendConfirmationEmailAsync(ResendConfirmationEmailRequest request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
                return Result.Success();

            if (user.EmailConfirmed)
                return Result.Failure(UserErrors.DuplicatedConfirmation);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            _logger.LogInformation("Confirmation code: {code}", code);



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
                var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
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

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            _logger.LogInformation("Reset code: {code}", code);

            return Result.Success();
        }
        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

    }
}
