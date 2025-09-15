using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using UserManagementApp.Models;
using UserManagementApp.Services;

namespace UserManagementApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IAuthService _authService;
        private readonly IAlertService _alertService;

        public LoginViewModel(IAuthService authService, IAlertService alertService)
        {
            _authService = authService;
            LoginCommand = new Command(async () => await LoginAsync());
            NavigateToRegisterCommand = new Command(async () => await Shell.Current.GoToAsync("///RegisterPage"));
            _alertService = alertService;
        }

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        private string _errorMessage = string.Empty;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public ICommand NavigateToRegisterCommand { get; private set; }

        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        public ICommand LoginCommand { get; }

        public async Task LoginAsync()
        {
            try
            {
                ErrorMessage = string.Empty;
                var request = new LoginRequest { Email = Email, Password = Password };
                var errors = Helpers.ValidationHelper.Validate(request);
                if (errors.Any())
                {
                    string message = string.Join("\n", errors);                    
                    await _alertService.ShowAlertAsync("Validation Failed!", message, "OK");
                    return;
                }

                var response = await _authService.LoginAsync(request);

                if (response?.AccessToken != null)
                {
                    // Store token securely
                    await SecureStorage.SetAsync("access_token", response.AccessToken);
                    await SecureStorage.SetAsync("refresh_token", response.RefreshToken??"NA");
                    await SecureStorage.SetAsync("user_email", request.Email);

                    // TODO: Navigate to home or store token
                    await Shell.Current.GoToAsync("//HomePage");
                }
                else
                {
                    ErrorMessage = "Invalid credentials.";
                    //NavigateToRegisterCommand = new Command(async () => await Shell.Current.GoToAsync("///RegisterPage"));
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Login failed: {ex.Message}";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}