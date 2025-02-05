using Microsoft.AspNetCore.Identity;
using Petsica.Shared.Contracts.Authrization;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations.Schema;
=======
>>>>>>> 329df3e52952832db9600b6bd3928ae61f3da4aa

namespace Petsica.Infrastructure.DBModel
{
    public sealed class ApplicationUser : IdentityUser
    {
<<<<<<< HEAD
        [NotMapped]
=======
>>>>>>> 329df3e52952832db9600b6bd3928ae61f3da4aa
        public List<RefreshToken> RefreshTokens { get; set; } = [];
    }
}
