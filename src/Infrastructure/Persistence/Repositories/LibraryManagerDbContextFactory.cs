using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Persistence.Repositories
{
    /* public class LibraryManagerDbContextFactory : IDesignTimeDbContextFactory<LibraryManagerDbContext>
    {
        public LibraryManagerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryManagerDbContext>();
            optionsBuilder.UseSqlite("Data Source=library.db");

            return new LibraryManagerDbContext(optionsBuilder.Options);
        }
    } */
}