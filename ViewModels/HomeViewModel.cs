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
    public class HomeViewModel : INotifyPropertyChanged
    {
        private readonly IAuthService _authService;
        private readonly IAlertService _alertService;

        public HomeViewModel(IAuthService authService, IAlertService alertService)
        {
            _authService = authService;
            _alertService = alertService;
            LogoutCommand = new Command(async () => await LogOutAsync());
        }

        public ICommand LogoutCommand { get; }

        public async Task LogOutAsync()
        {
            var refreshToken = await SecureStorage.GetAsync("refresh_token");
            var logOutRequest = new LogoutRequest { RefreshToken = refreshToken };

            bool isLoggedOut = await _authService.LogoutAsync(logOutRequest);
            if (isLoggedOut)
            {
                await Shell.Current.GoToAsync("///LoginPage");
            }
            else
            {
                await _alertService.ShowAlertAsync("Error!", "Failed to Logout!", "OK");
            }

        }    

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
