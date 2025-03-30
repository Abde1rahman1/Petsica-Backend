using Microsoft.AspNetCore.Identity;
using Petsica.Core.Entities.Authrization;

namespace Petsica.Infrastructure.DBModel
{
    public sealed class ApplicationUser : IdentityUser
    {
        public string? Photo { get; set; }
        public string? Address { get; set; }
        public string? NationalID { get; set; }
        public string? Type { get; set; }

        public string? ApprovalPhoto { get; set; }
        public bool IsApproval { get; set; }
        public bool IsDisabled { get; set; }


        public List<RefreshToken> RefreshTokens { get; set; } = [];
    }
}
