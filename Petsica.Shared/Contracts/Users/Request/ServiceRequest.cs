namespace Petsica.Shared.Contracts.Users.Request
{
    public record ServiceRequest(
    decimal Price,
    string Description,
    string Title,
    string Location
);
}
