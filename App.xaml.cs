using System.Text;
using Microsoft.Maui.Storage;
using System.Text.Json;
using UserManagementApp.Services;

namespace UserManagementApp
{
    public partial class App : Application
    {
        private readonly IAuthService _authService;

        public App(IAuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }


        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = new Window(new ContentPage()); // Temporary placeholder
            _ = InitializeAppAsync(window);
            return window;
        }

        private async Task InitializeAppAsync(Window window)
        {
            var shell = new AppShell();
            window.Page = shell;

            //await Task.Delay(100); // Give Shell time to initialize

            var token = await SecureStorage.GetAsync("access_token");

            if (!string.IsNullOrEmpty(token) && !IsTokenExpired(token))
            {
                await Shell.Current.GoToAsync("///HomePage");
            }
            else
            {
                var refreshed = await _authService.TryRefreshTokenAsync();

                if (refreshed)
                {
                    await Shell.Current.GoToAsync("///HomePage");
                }
                else
                {
                    SecureStorage.Remove("access_token");
                    SecureStorage.Remove("refresh_token");

                    await Shell.Current.GoToAsync("///LoginPage",true);
                }
            }
        }

        private static bool IsTokenExpired(string token)
        {
            var parts = token.Split('.');
            if (parts.Length != 3)
                return true;

            try
            {
                var payload = parts[1];
                var jsonBytes = Convert.FromBase64String(PadBase64(payload));
                var json = Encoding.UTF8.GetString(jsonBytes);

                var claims = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
                if (claims != null && claims.TryGetValue("exp", out var expValue))
                {
                    var exp = Convert.ToInt64(expValue);
                    var expiryDate = DateTimeOffset.FromUnixTimeSeconds(exp);
                    return expiryDate < DateTimeOffset.UtcNow;
                }
            }
            catch
            {
                return true; // If decoding fails, assume expired
            }

            return true;
        }

        private static string PadBase64(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: return base64 + "==";
                case 3: return base64 + "=";
                default: return base64;
            }
        }

    }
}