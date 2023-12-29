using Domain.Repositories;
using Infrastructure;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private Lazy<IBookRepository> _lazyBookRepository;
        private Lazy<IUnitOfWork> _lazyUnitOfWork;

        public RepositoryManager(LibraryManagerDbContext dbContext)
        {
            _lazyBookRepository = new Lazy<IBookRepository>(() => new BookRepository(dbContext));
            _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext));
        }

        public IBookRepository Book => _lazyBookRepository.Value;
        public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
    }
}
