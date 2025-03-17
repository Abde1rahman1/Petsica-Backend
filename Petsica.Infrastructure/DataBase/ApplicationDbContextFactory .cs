using Microsoft.EntityFrameworkCore.Design;


namespace Petsica.Infrastructure.DataBase;


public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectModels;Database=Petsicaa;Trusted_Connection=True;Encrypt=False");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}


