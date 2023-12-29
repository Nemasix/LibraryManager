using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class LoanNotFoundException :
        NotFoundException
    {
        public LoanNotFoundException(Guid id)
            : base($"The loan with id {id} was not found.")
        {
        }
    }
}
