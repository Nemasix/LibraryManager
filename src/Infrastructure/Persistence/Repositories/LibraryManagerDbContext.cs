using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.Repositories
{
    public sealed class LibraryManagerDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }


        public LibraryManagerDbContext(DbContextOptions<LibraryManagerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryManagerDbContext).Assembly);
        }
    }
}