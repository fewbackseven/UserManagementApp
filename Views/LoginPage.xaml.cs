using UserManagementApp.Services;
using UserManagementApp.ViewModels;

namespace UserManagementApp.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(IAuthService authService)
    {
        InitializeComponent();
        BindingContext = new LoginViewModel(authService);
    }
}
