namespace Petsica.Shared.Contracts.Users.Response
{
    public record AddSitterServiceResponse(
    int ServiceID,
    string SitterID,
    decimal Price,
    string Description,
    string Title,
    string Location
);
}
