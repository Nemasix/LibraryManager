using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class BooksNotFoundException
        : NotFoundException
    {
        public BooksNotFoundException(Guid id)
            : base($"The book with id {id} was not found.")
        {
        }
    }
}