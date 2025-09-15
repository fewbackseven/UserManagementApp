using UserManagementApp.Services;
using UserManagementApp.ViewModels;

namespace UserManagementApp.Views
{
    public partial class RegisterPage : ContentPage
    {

        public RegisterPage(IAuthService authService, IAlertService alertService)
        {
            InitializeComponent();
            BindingContext = new RegisterViewModel(authService,alertService);
        }

    }
}