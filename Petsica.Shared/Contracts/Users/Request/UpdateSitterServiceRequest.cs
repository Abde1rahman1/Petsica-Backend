namespace Petsica.Shared.Contracts.Users.Request
{
    public record UpdateSitterServiceRequest(
int ServiceID,
decimal Price,
string Description,
string Title,
string Location
);
}
