using UserManagementApp.Services;
using UserManagementApp.ViewModels;

namespace UserManagementApp.Views
{
    public partial class RegisterPage : ContentPage
    {

        public RegisterPage(AuthService authService)
        {
            InitializeComponent();
            BindingContext = new RegisterViewModel(authService);
        }

    }
}