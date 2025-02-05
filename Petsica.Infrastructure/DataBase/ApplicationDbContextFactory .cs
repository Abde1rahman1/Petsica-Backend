<<<<<<< HEAD
﻿using Microsoft.EntityFrameworkCore.Design;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
>>>>>>> 329df3e52952832db9600b6bd3928ae61f3da4aa

namespace Petsica.Infrastructure.DataBase;


public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

<<<<<<< HEAD
        optionsBuilder.UseSqlServer("Server=.;Database=Petsica;Trusted_Connection=True;Encrypt=False");
=======
        optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER01;Database=Petsica;Trusted_Connection=True;TrustServerCertificate=True;");
>>>>>>> 329df3e52952832db9600b6bd3928ae61f3da4aa

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
