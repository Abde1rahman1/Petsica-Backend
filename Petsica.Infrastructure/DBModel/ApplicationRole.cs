using Microsoft.AspNetCore.Identity;

namespace Petsica.Infrastructure.DBModel
{
    public sealed class ApplicationRole : IdentityRole
    {
        public bool IsDefault { get; set; }
        public bool IsDelete { get; set; }

    }
}
