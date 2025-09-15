using UserManagementApp.Services;
using UserManagementApp.ViewModels;

namespace UserManagementApp.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(IAuthService authService, IAlertService alertService)
    {
        InitializeComponent();
        BindingContext = new LoginViewModel(authService, alertService);
    }
}
