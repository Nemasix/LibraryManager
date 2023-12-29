using Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<UserDto> CreateAsync(UserForCreationDto userDto, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid id, UserForUpdateDto userDto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
