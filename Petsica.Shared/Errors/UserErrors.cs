using Microsoft.AspNetCore.Http;

namespace Petsica.Shared.Error
{
    public static class UserErrors
    {
        public static readonly Errors InvalidCredentials =
            new("User.InvalidCredentials", "Invalid email/password", StatusCodes.Status401Unauthorized);

        public static readonly Errors InvalidJwtToken =
            new("User.InvalidJwtToken", "Invalid Jwt token", StatusCodes.Status401Unauthorized);

        public static readonly Errors InvalidRefreshToken =
            new("User.InvalidRefreshToken", "Invalid refresh token", StatusCodes.Status401Unauthorized);

        public static readonly Errors DuplicatedEmail =
            new("User.DuplicatedEmail", "Another user with the same email is already exists", StatusCodes.Status409Conflict);

        public static readonly Errors EmailNotConfirmed =
            new("User.EmailNotConfirmed", "Email is not confirmed", StatusCodes.Status401Unauthorized);

        public static readonly Errors InvalidCode =
            new("User.InvalidCode", "Invalid code", StatusCodes.Status401Unauthorized);

        public static readonly Errors DuplicatedConfirmation =
            new("User.DuplicatedConfirmation", "Email already confirmed", StatusCodes.Status400BadRequest);

        public static readonly Errors InvalidCreateService =
           new("User.InvalidCreateService", "Can not create service Mating/Adoption", StatusCodes.Status400BadRequest);

        public static readonly Errors InvalidChooesService =
          new("User.InvalidChooesService", "Can not Chooes Service Mating/Adoption", StatusCodes.Status400BadRequest);
    }
}
