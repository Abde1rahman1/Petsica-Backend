namespace Petsica.Shared.Contracts.Users.Response
{
    public record UserProfileResponse(
    string Email,
    string UserName,
    string Photo,
    string Address
);
}
