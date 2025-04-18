﻿using System.ComponentModel.DataAnnotations;

namespace Petsica.Shared.Email.Settings
{
    public static class MailSettings
    {
        [Required, EmailAddress]
        public const string Mail = "emmy89@ethereal.email";

        [Required]
        public const string DisplayName = "Petsica";

        [Required]
        public const string Password = "rQAcr1VxJHFSPttBWe";

        [Required]
        public const string Host = "smtp.ethereal.email";

        [Range(100, 999)]
        public const int Port = 587;
    }
}
