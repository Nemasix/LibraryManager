using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Repositories
{
    public sealed class LibraryManagerDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }


        public LibraryManagerDbContext(DbContextOptions options) : base(options)
        {
        }

        public static LibraryManagerDbContext Create(IMongoDatabase database) =>
        new(new DbContextOptionsBuilder<LibraryManagerDbContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryManagerDbContext).Assembly);
        }
    }
}