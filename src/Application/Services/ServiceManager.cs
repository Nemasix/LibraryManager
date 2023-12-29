using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Repositories;

namespace Application.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookService> _lazyBookService;
        private readonly Lazy<ILoanService> _lazyLoanService;
        private readonly Lazy<IUserService> _lazyUserService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _lazyBookService = new Lazy<IBookService>(() => new BookService(repositoryManager));
            _lazyLoanService = new Lazy<ILoanService>(() => new LoanService(repositoryManager));
            _lazyUserService = new Lazy<IUserService>(() => new UserService(repositoryManager));    
        }

        public IBookService BookService => _lazyBookService.Value;
        public ILoanService LoanService => _lazyLoanService.Value;
        public IUserService UserService => _lazyUserService.Value;
    }
}