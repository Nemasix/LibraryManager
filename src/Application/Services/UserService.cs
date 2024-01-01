using Application.Abstractions;
using Application.Contracts;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _repositoryManager;

        public UserService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<UserDto> CreateAsync(UserForCreationDto userForCreationDto, CancellationToken cancellationToken = default)
        {
            var user = userForCreationDto.Adapt<Domain.Entities.User>();
            _repositoryManager.User.Add(user);
            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            return user.Adapt<UserDto>();
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            _repositoryManager.User.Delete(id);
            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var users = await _repositoryManager.User.GetAllAsync(cancellationToken);

            return users.Adapt<IEnumerable<UserDto>>();
        }

        public async Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _repositoryManager.User.GetByIdAsync(id, cancellationToken);
            if (user == null)
            {
                throw new UserNotFoundException(id);
            }
            return user.Adapt<UserDto>();
        }

        public async Task UpdateAsync(Guid id, UserForUpdateDto userDto, CancellationToken cancellationToken = default)
        {
            var user = await _repositoryManager.User.GetByIdAsync(id, cancellationToken);
            if(user == null)
            {
                throw new UserNotFoundException(id);
            }

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            user.Password = userDto.Password;

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
