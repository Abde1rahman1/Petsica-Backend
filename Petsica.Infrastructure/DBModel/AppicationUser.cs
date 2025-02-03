using Microsoft.AspNetCore.Identity;
using Petsica.Shared.Contracts.Authrization;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petsica.Infrastructure.DBModel
{
    public sealed class ApplicationUser : IdentityUser
    {
        [NotMapped]
        public List<RefreshToken> RefreshTokens { get; set; } = [];
    }
}
