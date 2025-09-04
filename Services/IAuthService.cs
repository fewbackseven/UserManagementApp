using UserManagementApp.Models;

namespace UserManagementApp.Services
{
    public interface IAuthService
    {
        Task LogoutAllAsync();
        Task<bool> TryRefreshTokenAsync();
        Task LogoutAsync(LogoutRequest request);
        Task<AuthResponse?> LoginAsync(LoginRequest request);
        Task<AuthResponse?> RegisterAsync(RegisterRequest request);
    }
}
