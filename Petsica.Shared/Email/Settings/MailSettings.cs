using System.ComponentModel.DataAnnotations;

namespace Petsica.Shared.Email.Settings
{
    public static class MailSettings
    {
        [Required, EmailAddress]
        public const string Mail = "petsicaofficial@gmail.com";

        [Required]
        public const string DisplayName = "Petsica";

        [Required]
        public const string Password = "piyn uvxb xxve axjx";

        [Required]
        public const string Host = "smtp.gmail.com";

        [Range(100, 999)]
        public const int Port = 587;
    }
}
