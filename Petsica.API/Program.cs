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

			app.UseCors();  

			app.MapControllers();

			// Map SignalR hubs
			app.MapHub<ChatHub>("/chatHub").RequireCors("AllowFrontend");
			app.MapHub<UserChatHub>("/userChatHub").RequireCors("AllowFrontend");

			app.UseExceptionHandler("/Home/Error");

			app.Run();
		}
	}
}
