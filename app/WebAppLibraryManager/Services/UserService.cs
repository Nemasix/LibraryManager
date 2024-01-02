using WebAppLibraryManager.Contracts;

namespace WebAppLibraryManager.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration Configuration;
        private readonly ApiClient ApiClient;
        public UserService(IConfiguration configuration)
        {
            Configuration = configuration;
            ApiClient = new ApiClient(Configuration);
        }

        public async Task<UserDto> GetUserAsync(Guid id)
        {
            return await ApiClient.GetAsync<UserDto>($"users/{id}");
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            return await ApiClient.GetAsync<IEnumerable<UserDto>>("users");
        }

        public async Task<UserDto> CreateUserAsync(UserForCreationDto user)
        {
            return await ApiClient.PostAsync<UserForCreationDto, UserDto>("users", user);
        }

        public async Task UpdateUserAsync(Guid id, UserForUpdateDto user)
        {
            await ApiClient.PutAsync<UserForUpdateDto>($"users/{id}", user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await ApiClient.DeleteAsync($"users/{id}");
        }
    }
}
