using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using UserManagementApp.Models;

namespace UserManagementApp.Services
{
    public class AuthService:IAuthService
    {

        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AuthResponse?> RegisterAsync(RegisterRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/auth/register", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AuthResponse>();
        }

        public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            Console.WriteLine(_httpClient.BaseAddress);
            var response = await _httpClient.PostAsJsonAsync("/api/auth/login", request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {response.StatusCode}, Content: {content}");
            }

            return await response.Content.ReadFromJsonAsync<AuthResponse>();
        }

        public async Task LogoutAsync(LogoutRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/auth/logout", request);

            if (response.IsSuccessStatusCode)
            {
                SecureStorage.Remove("access_token");
                SecureStorage.Remove("refresh_token");

                // Optionally navigate to login
                await Shell.Current.GoToAsync("///LoginPage");
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> TryRefreshTokenAsync()
        {
            var refreshToken = await SecureStorage.GetAsync("refresh_token");
            if (string.IsNullOrEmpty(refreshToken))
                return false;

            var request = new RefreshRequest { RefreshToken = refreshToken };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/auth/refresh", request);

                if (response.IsSuccessStatusCode)
                {
                    var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
                    if (authResponse != null)
                    {
                        await SecureStorage.SetAsync("access_token", authResponse.AccessToken);
                        await SecureStorage.SetAsync("refresh_token", authResponse.RefreshToken);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Refresh token failed: {ex.Message}");
            }

            return false;
        }

        public async Task LogoutAllAsync()
        {
            var response = await _httpClient.PostAsync("/api/auth/logout-all", null);
            response.EnsureSuccessStatusCode();
        }

    }
}
