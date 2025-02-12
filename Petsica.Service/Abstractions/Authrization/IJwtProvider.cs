using Petsica.Infrastructure.DBModel;

namespace Petsica.Service.Service.Authrization
{
    public interface IJwtProvider
    {
        (string token, int expiresIn) GenerateToken(ApplicationUser user);
        string? ValidateToken(string token);
    }
}
