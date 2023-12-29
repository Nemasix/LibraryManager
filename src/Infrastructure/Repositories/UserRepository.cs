using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly LibraryManagerDbContext _dbContext;
        public UserRepository(LibraryManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(User user)
        {
            _dbContext.Users.Add(user);
        }

        public void Delete(Guid id)
        {
            _dbContext.Users.Remove(new User { Id = id });
        }

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken ct = default)
        {
            return await _dbContext.Users.ToListAsync(ct);
        }

        public async Task<User> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, ct);
        }
    }
}
