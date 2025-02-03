using Microsoft.AspNetCore.Identity;
using Petsica.Shared.Contracts.Authrization;

namespace Petsica.Infrastructure.DBModel
{
    public sealed class ApplicationUser : IdentityUser
    {
        public List<RefreshToken> RefreshTokens { get; set; } = [];
    }
}
