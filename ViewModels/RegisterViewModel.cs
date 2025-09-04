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

        private readonly AuthService _authService;

        public RegisterViewModel(AuthService authService)
        {
            _authService = authService;
            RegisterCommand = new Command(async () => await RegisterAsync());
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

                var response = await _authService.RegisterAsync(request);

                if (response?.AccessToken != null)
                {
                    await Shell.Current.GoToAsync("///HomePage");
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
