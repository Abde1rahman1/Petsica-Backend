namespace Petsica.Shared.Contracts.Users.Request
{
    public record ChangePasswordRequest(
       string CurrentPassword,
       string NewPassword
   );
}
