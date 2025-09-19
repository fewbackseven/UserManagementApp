using System;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using UserManagementApp.Services;

namespace UserManagementApp.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private readonly IUserProfileService _profileService;
        private readonly IAlertService _alertService;

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // Properties
        private Guid _id;
        public Guid Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsFormValid));
            }
        }

        private string _mobile;
        public string Mobile
        {
            get => _mobile;
            set
            {
                if (_mobile == value) return;
                _mobile = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsFormValid));
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                if (_email == value) return;
                _email = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _dateOfBirth;
        public DateTime? DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (_dateOfBirth == value) return;
                _dateOfBirth = value;
                OnPropertyChanged();
            }
        }

        private DateTime _anniversary = DateTime.Today;
        public DateTime Anniversary
        {
            get => _anniversary;
            set
            {
                if (_anniversary == value) return;
                _anniversary = value;
                OnPropertyChanged();
            }
        }

        private string _gender;
        public string Gender
        {
            get => _gender;
            set
            {
                if (_gender == value) return;
                _gender = value;
                OnPropertyChanged();
            }
        }

        // Computed property for button enable/disable
        public bool IsFormValid => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Mobile);

        // Commands
        public ICommand ChangeMobileCommand { get; }
        public ICommand ChangeEmailCommand { get; }
        public ICommand UpdateProfileCommand { get; }

        public ProfileViewModel(IUserProfileService profileService, IAlertService alertService)
        {
            _profileService = profileService;
            _alertService = alertService;
            ChangeMobileCommand = new Command(OnChangeMobile);
            ChangeEmailCommand = new Command(OnChangeEmail);
            UpdateProfileCommand = new Command(OnUpdateProfile);
        }

        public async Task LoadProfileAsync()
        {
            var email_id = await SecureStorage.GetAsync("user_email");
            var profile = await _profileService.GetUserProfileAsync(email_id);
            if (profile != null)
            {
                Id = profile.Id;
                Email = profile.Email;
                Name = profile.FirstName + " " + profile.LastName;               
                Gender = profile.Gender;
                DateOfBirth = profile.DateOfBirth?? DateTime.MinValue ;
                Mobile = profile.PhoneNumber;               
            }
        }

        private void OnChangeMobile()
        {
            // Logic to change mobile (e.g., open dialog or navigate)
        }

        private void OnChangeEmail()
        {
            // Logic to change email
        }

        private async void OnUpdateProfile()
        {
            await Shell.Current.GoToAsync("///UserprofilePage");            
        }
    }
}