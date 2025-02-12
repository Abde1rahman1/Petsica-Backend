namespace Petsica.Shared.Contracts.Authrization.Request
{
    public record ResetPasswordRequest(
    string Email,
    string Code,
    string NewPassword
);
}
