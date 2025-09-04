using UserManagementApp.Models;

namespace UserManagementApp.Services
{
        public interface IUserProfileService
        {
            Task<UserProfileResponse?> GetUserProfileAsync(string email);
            Task<bool> CreateUserProfileAsync(UserProfileRequest profile);
            Task<bool> UpdateUserProfileAsync(string Id, UserProfileRequest profile);
        }
}
