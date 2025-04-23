using Microsoft.EntityFrameworkCore.Design;


namespace Petsica.Infrastructure.DataBase;


public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        //Hamed
        //optionsBuilder.UseSqlServer("Server=.;Database=Petsica;Trusted_Connection=True;Encrypt=False");

        //Sanaa
        //optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectModels;Database=Petsica;Trusted_Connection=True;Encrypt=False");

        //Abdoo
        optionsBuilder.UseSqlServer("Server=.;Database=Petsicaa;Trusted_Connection=True;Encrypt=False");
        //optionsBuilder.UseSqlServer("Server=.;Database=Petsicaa;Trusted_Connection=True;Encrypt=False");

        //Public
        //optionsBuilder.UseSqlServer("Server=db16824.public.databaseasp.net; Database=db16824; User Id=db16824; Password=7f-Q@Aj6n3L?; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True; ");
        optionsBuilder.UseSqlServer("Server=db16824.public.databaseasp.net; Database=db16824; User Id=db16824; Password=7f-Q@Aj6n3L?; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True; ");


        return new ApplicationDbContext(optionsBuilder.Options);
    }
}


