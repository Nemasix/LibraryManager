using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class LibraryManagerDbContextFactory : IDesignTimeDbContextFactory<LibraryManagerDbContext>
    {
        public LibraryManagerDbContext CreateDbContext(string[] args)
        {
            var mongoConnectionString = "mongodb://localhost:27017";
            var mongoClient = new MongoClient(mongoConnectionString);
            return LibraryManagerDbContext.Create(mongoClient.GetDatabase("LibraryManager"));
        }
    }
}
