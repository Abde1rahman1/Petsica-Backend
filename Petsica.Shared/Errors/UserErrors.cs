using Microsoft.AspNetCore.Http;

namespace Petsica.Shared.Error
{
    public static class UserErrors
    {
        public static readonly Errors InvalidCredentials =
            new("User.InvalidCredentials", "Invalid email/password", StatusCodes.Status401Unauthorized);

        public static readonly Errors DisabledUser =
            new("User.DisabledUser", "Disabled user, please contact your administrator", StatusCodes.Status401Unauthorized);

        public static readonly Errors LockedUser =
            new("User.LockedUser", "Locked user, please contact your administrator", StatusCodes.Status401Unauthorized);

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
           new("User.InvalidCreateService", "Can not create service ", StatusCodes.Status400BadRequest);

        public static readonly Errors InvalidChooesService =
          new("User.InvalidChooesService", "Can not Chooes Service ", StatusCodes.Status400BadRequest);

        public static readonly Errors NoServicesYet =
         new("User.NoServicesYet", "There are no services available yet", StatusCodes.Status400BadRequest);

        public static readonly Errors ServiceNotFound =
          new("User.ServiceNotFound", "There are no services available yet", StatusCodes.Status400BadRequest);

        public static readonly Errors NotApproval =
           new("User.NotApproval", "user not Approval yet ", StatusCodes.Status400BadRequest);

        public static readonly Errors InvalidType =
            new("User.InvalidType", "this type is Invalid", StatusCodes.Status400BadRequest);
        public static readonly Errors ExistNotEmail =
           new("User.ExistNotEmail", " email is Not exists", StatusCodes.Status409Conflict);
        public static readonly Errors UserNotFound =
          new("Pet.UserNotFound", "No User was found with the given ID", StatusCodes.Status404NotFound);

    }
}
