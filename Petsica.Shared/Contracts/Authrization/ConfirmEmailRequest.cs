namespace Petsica.Shared.Contracts.Authrization
{
    public record ConfirmEmailRequest(
    string UserId,
    string Code
);
}
