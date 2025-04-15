namespace Petsica.Shared.Contracts.Authrization.Request
{
    public record RegisterRequest(
    string UserName,
    string Email,
    string Password,
    string Photo,
    string Address,
    string Type,
    string ApprovalPhoto,
    string NationalID
);
}
