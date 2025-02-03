namespace Petsica.Shared.Contracts.Authrization
{
    public record AuthResponse(
        string Id,
        string? Email,
        string Token,
        int ExpiresIn,
        string RefreshToken,
        DateTime RefreshTokenExpiration
    );
}
