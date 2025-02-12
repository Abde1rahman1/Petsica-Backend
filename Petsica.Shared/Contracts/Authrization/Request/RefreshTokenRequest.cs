namespace Petsica.Shared.Contracts.Authrization.Request
{
    public record RefreshTokenRequest(
    string Token,
    string RefreshToken
);
}
