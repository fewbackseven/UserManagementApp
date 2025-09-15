using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UserManagementApp.Models;
using UserManagementApp.Services;

namespace UserManagementApp.ViewModels
{
    public class RegisterViewModel:INotifyPropertyChanged
    {

        private readonly IAuthService _authService;
        private readonly IAlertService _alertService;

        public RegisterViewModel(IAuthService authService, IAlertService alertService)
        {
            _authService = authService;
            RegisterCommand = new Command(async () => await RegisterAsync());
            _alertService = alertService;
        }

        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        private string _errorMessage = string.Empty;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        public ICommand RegisterCommand { get; }

        public async Task RegisterAsync()
        {
            try
            {
                ErrorMessage = string.Empty;
                var request = new RegisterRequest
                {
                    Email = Email,
                    UserName = UserName,
                    Password = Password
                };
                
                var errors = Helpers.ValidationHelper.Validate(request);
                if (errors.Any())
                {
                    string message = string.Join("\n", errors);
                    await _alertService.ShowAlertAsync("Oops! Validation Failed.", message, "OK");
                    return;
                }

                //TODO: Show validation errors from Backend
                var response = await _authService.RegisterAsync(request);

                if (response?.AccessToken != null)
                {
                    await SecureStorage.SetAsync("access_token", response.AccessToken);
                    await SecureStorage.SetAsync("refresh_token", response.RefreshToken ?? "NA");
                    await SecureStorage.SetAsync("user_email", request.Email);

                    await Shell.Current.GoToAsync("//HomePage");
                }
                else
                {
                    ErrorMessage = "Registration failed.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error: {ex.Message}";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
