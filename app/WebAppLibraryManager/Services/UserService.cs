﻿using WebAppLibraryManager.Contracts;

namespace WebAppLibraryManager.Services
{
    public class UserService : IUserService
    {
        private IConfiguration Configuration;
        private ApiClient ApiClient;
        public UserService(IConfiguration configuration)
        {
            Configuration = configuration;
            ApiClient = new ApiClient(Configuration);
        }

        public async Task<UserDto> GetUserAsync(Guid id)
        {
            return await ApiClient.GetAsync<UserDto>($"user/{id}");
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            return await ApiClient.GetAsync<IEnumerable<UserDto>>("user");
        }

        public async Task<UserDto> CreateUserAsync(UserForCreationDto user)
        {
            return await ApiClient.PostAsync<UserDto>("user", user);
        }

        public async Task<UserDto> UpdateUserAsync(Guid id, UserForUpdateDto user)
        {
            return await ApiClient.PutAsync<UserDto>($"user/{id}", user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await ApiClient.DeleteAsync($"user/{id}");
        }
    }
}