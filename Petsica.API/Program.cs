using Hangfire;
using HangfireBasicAuthenticationFilter;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Petsica.Shared;
using Petsica.Shared.Hubs;
using Serilog;

namespace Petsica.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDependencies(builder.Configuration);

            builder.Services.AddSignalR();

            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration)
            );

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                Authorization =
                [
                     new HangfireCustomBasicAuthenticationFilter
                     {
                          User = app.Configuration.GetValue<string>("HangfireSettings:Username"),
                          Pass = app.Configuration.GetValue<string>("HangfireSettings:Password")
                     }
                    ],
                DashboardTitle = "Petsica Dashboard",
            });

            app.UseCors("AllowFrontend");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            //Map Health Checks
            app.MapHealthChecks("health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            // Map SignalR hubs

            app.MapHub<ChatHub>("/chatHub").RequireCors("AllowFrontend");
            app.MapHub<ClinicChatHub>("/clinicChatHub").RequireCors("AllowFrontend");
            app.MapHub<UserChatHub>("/userChatHub").RequireCors("AllowFrontend");

            app.UseExceptionHandler("/Home/Error");

            app.Run();
        }
    }
}
