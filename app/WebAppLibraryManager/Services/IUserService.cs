using WebAppLibraryManager.Contracts;

namespace WebAppLibraryManager.Services
{
    public interface IUserService
    {

        Task<UserDto> GetUserAsync(Guid id);

        Task<IEnumerable<UserDto>> GetUsersAsync();

        Task<UserDto> CreateUserAsync(UserForCreationDto user);

        Task UpdateUserAsync(Guid id, UserForUpdateDto user);

        Task DeleteUserAsync(Guid id);
    }
}
