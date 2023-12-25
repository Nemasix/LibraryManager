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

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _lazyBookService = new Lazy<IBookService>(() => new BookService(repositoryManager));
        }

        public IBookService BookService => _lazyBookService.Value;
    }
}