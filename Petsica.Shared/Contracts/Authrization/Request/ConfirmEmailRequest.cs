namespace Petsica.Shared.Contracts.Authrization.Request
{
    public record ConfirmEmailRequest(
    string UserId,
    string Code
);
}
