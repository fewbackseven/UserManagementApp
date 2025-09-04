using System.Net.Http.Json;
using UserManagementApp.Models;

namespace UserManagementApp.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly HttpClient _httpClient;

        public UserProfileService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private async Task AddAuthHeaderAsync()
        {
            var token = await SecureStorage.GetAsync("access_token");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<UserProfileResponse?> GetUserProfileAsync(string email)
        {
            try
            {
                await AddAuthHeaderAsync();
                return await _httpClient.GetFromJsonAsync<UserProfileResponse>($"/api/userprofile/email/{email}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> CreateUserProfileAsync(UserProfileRequest profile)
        {
            try
            {
                //TO DO: Create profile not used
                var response = await _httpClient.PostAsJsonAsync("api/userprofile/", profile);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateUserProfileAsync(string Id, UserProfileRequest profile)
        {
            try
            {
                await AddAuthHeaderAsync();
                var response = await _httpClient.PutAsJsonAsync($"api/userprofile/{Id}", profile);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}