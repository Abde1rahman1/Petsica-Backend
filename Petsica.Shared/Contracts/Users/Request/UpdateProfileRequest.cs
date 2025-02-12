namespace Petsica.Shared.Contracts.Users.Request
{
    public record UpdateProfileRequest(
    string UserName,
    string Photo,
    string Address,
    string Location
);
}
