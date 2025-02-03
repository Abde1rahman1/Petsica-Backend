using Microsoft.EntityFrameworkCore.Design;

namespace Petsica.Infrastructure.DataBase;


public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        optionsBuilder.UseSqlServer("Server=.;Database=Petsica;Trusted_Connection=True;Encrypt=False");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
