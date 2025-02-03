namespace Petsica.Shared.Contracts.Authrization
{
    public record RefreshTokenRequest(
    string Token,
    string RefreshToken
);
}
