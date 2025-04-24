using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Petsica.Shared.Email.Settings;


namespace Petsica.Service.Health;

public class MailProviderHealthCheck : IHealthCheck
{


    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            using var smtp = new SmtpClient();
            smtp.Connect(MailSettings.Host, MailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(MailSettings.Mail, MailSettings.Password);

            return await Task.FromResult(HealthCheckResult.Healthy());
        }
        catch (Exception exception)
        {
            return await Task.FromResult(HealthCheckResult.Unhealthy(exception: exception));
        }
    }
}