using UserManagementApp.Services;
using UserManagementApp.ViewModels;

namespace UserManagementApp.Views;

public partial class HomePage : ContentPage
{
    private readonly HomeViewModel _viewModel;

    public HomePage(IAuthService authService, IAlertService alertService)
	{
		InitializeComponent();
        _viewModel = new HomeViewModel(authService, alertService);
        BindingContext = _viewModel;
    }    
}