using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public sealed class BookRepository : IBookRepository
    {
        private readonly LibraryManagerDbContext _dbContext;
        public BookRepository(LibraryManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Book book)
        {
            _dbContext.Books.Add(book);
        }

        public void Delete(Guid id)
        {
            _dbContext.Books.Remove(new Book { Id = id });
        }

        public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken ct = default)
        {
            return await _dbContext.Books.ToListAsync(ct);
        }

        public async Task<IEnumerable<Book>> GetAllByOwnerAsync(Guid ownerId, CancellationToken ct = default)
        {
            return await _dbContext.Books.Where(b => b.OwnerId == ownerId).ToListAsync(ct);
        }

        public async Task<Book> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == id, ct);
        }
    }
}