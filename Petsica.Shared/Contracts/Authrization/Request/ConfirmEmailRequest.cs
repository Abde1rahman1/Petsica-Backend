namespace Petsica.Shared.Contracts.Authrization.Request
{
    public record ConfirmEmailRequest(
    string Email,
    string Code
);
}
