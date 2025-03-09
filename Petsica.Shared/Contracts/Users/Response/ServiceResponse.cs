namespace Petsica.Shared.Contracts.Users.Response
{
    public record ServiceResponse(
    int ServiceID,
    string SitterID,
    decimal Price,
    string Description,
    string Title,
    string Location
);
}
