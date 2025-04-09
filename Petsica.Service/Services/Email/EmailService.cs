using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Petsica.Shared.Email.Settings;

namespace Petsica.Service.Services.Email
{
    public class EmailService(/*IOptions<MailSettings> mailSettings, */ILogger<EmailService> logger) : IEmailSender
    {
        // private readonly MailSettings _mailSettings = mailSettings.Value;
        private readonly ILogger<EmailService> _logger = logger;

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage
            {
                Sender = MailboxAddress.Parse(MailSettings.Mail),
                Subject = subject
            };

            message.To.Add(MailboxAddress.Parse(email));

            var builder = new BodyBuilder
            {
                HtmlBody = htmlMessage
            };

            message.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            _logger.LogInformation("Sending email to {email}", email);

            smtp.Connect(MailSettings.Host, MailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(MailSettings.Mail, MailSettings.Password);
            await smtp.SendAsync(message);
            smtp.Disconnect(true);
        }
    }
}
