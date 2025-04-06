using Microsoft.EntityFrameworkCore.Design;


namespace Petsica.Infrastructure.DataBase;


public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        //Hamed
        //optionsBuilder.UseSqlServer("Server=.;Database=Petsica;Trusted_Connection=True;Encrypt=False");
		// optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectModels;Database=Petsicaa;Trusted_Connection=True;Encrypt=False");

		//Abdoo
		optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectModels;Database=Petsica;Trusted_Connection=True;Encrypt=False");

		//Public
		//optionsBuilder.UseSqlServer("Server=db15370.public.databaseasp.net; Database=db15370; User Id=db15370; Password=X%p4b7?WD-o5; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True; ");

		return new ApplicationDbContext(optionsBuilder.Options);
    }
}


