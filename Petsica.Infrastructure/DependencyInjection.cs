using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Petsica.Infrastructure;
using Petsica.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Infrastructure;
public static class DependencyInjection
{
	public static IServiceCollection AddDependencies(this IServiceCollection services,
			IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("DefaultConnection") ??
		   throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

		services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(connectionString));

		return services;
	}

	private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
	{
		var mappingConfig = TypeAdapterConfig.GlobalSettings;
		mappingConfig.Scan(Assembly.GetExecutingAssembly());

		services.AddSingleton<IMapper>(new Mapper(mappingConfig));

		return services;
	}
}
