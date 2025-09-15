using UserManagementApp.Services;
using UserManagementApp.ViewModels;

namespace UserManagementApp.Views
{
    public partial class UserprofilePage : ContentPage
    {
        private readonly UserProfileViewModel _viewModel;

        public UserprofilePage(IUserProfileService userProfileService, IAlertService alertService)
        {
            InitializeComponent();
            _viewModel = new UserProfileViewModel(userProfileService, alertService);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadProfileAsync();
        }

    }
}