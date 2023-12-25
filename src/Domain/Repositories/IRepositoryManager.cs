using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IRepositoryManager
    {
        IBookRepository Book { get; }
        IUnitOfWork UnitOfWork { get; } 
    }
}