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
        private Lazy<ILoanRepository> _lazyLoanRepository;
        private Lazy<IUserRepository> _lazyUserRepository;
        private Lazy<IUnitOfWork> _lazyUnitOfWork;

        public RepositoryManager(LibraryManagerDbContext dbContext)
        {
            _lazyBookRepository = new Lazy<IBookRepository>(() => new BookRepository(dbContext));
            _lazyLoanRepository = new Lazy<ILoanRepository>(() => new LoanRepository(dbContext));
            _lazyUserRepository = new Lazy<IUserRepository>(() => new UserRepository(dbContext));
            _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext));
        }

        public IBookRepository Book => _lazyBookRepository.Value;
        public ILoanRepository Loan => _lazyLoanRepository.Value;
        public IUserRepository User => _lazyUserRepository.Value;
        public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
    }
}
