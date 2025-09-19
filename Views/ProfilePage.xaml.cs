using UserManagementApp.Services;
using UserManagementApp.ViewModels;

namespace UserManagementApp.Views
{
	public partial class ProfilePage : ContentPage
    {
        private readonly ProfileViewModel _viewModel;

        public ProfilePage(IUserProfileService userProfileService, IAlertService alertService)
        {
            InitializeComponent();
            _viewModel = new ProfileViewModel(userProfileService, alertService);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadProfileAsync();
        }
    }
}