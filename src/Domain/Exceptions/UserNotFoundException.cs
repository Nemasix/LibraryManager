using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class UserNotFoundException :
        NotFoundException
    {
        public UserNotFoundException(Guid id)
            : base($"The user with id {id} was not found.")
        {
        }
    }
}
