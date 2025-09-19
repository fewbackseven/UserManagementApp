using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using UserManagementApp.Models;
using UserManagementApp.Services;

namespace UserManagementApp.ViewModels
{
    public partial class UserProfileViewModel : INotifyPropertyChanged
    {
        private readonly IUserProfileService _profileService;
        private readonly IAlertService _alertService;

        public UserProfileViewModel(IUserProfileService profileService, IAlertService alertService)
        {
            _profileService = profileService;            
            _alertService = alertService;
            SaveCommand = new Command(async () => await SaveProfileAsync());
            CancelCommand = new Command(async () => await CancelUpdateAsync());
        }

        private async Task CancelUpdateAsync()
        {
            await Shell.Current.GoToAsync("//ProfilePage");
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        // Properties
        private Guid _id;
        public Guid Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(); }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(); }
        }

        private string _gender;
        public string Gender
        {
            get => _gender;
            set { _gender = value; OnPropertyChanged(); }
        }

        private DateTime? _dateOfBirth;
        public DateTime? DateOfBirth
        {
            get => _dateOfBirth;
            set { _dateOfBirth = value; OnPropertyChanged(); }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set { _phoneNumber = value; OnPropertyChanged(); }
        }

        private string _alternatePhoneNumber;
        public string AlternatePhoneNumber
        {
            get => _alternatePhoneNumber;
            set { _alternatePhoneNumber = value; OnPropertyChanged(); }
        }

        private string _addressLine1;
        public string AddressLine1
        {
            get => _addressLine1;
            set { _addressLine1 = value; OnPropertyChanged(); }
        }

        private string _addressLine2;
        public string AddressLine2
        {
            get => _addressLine2;
            set { _addressLine2 = value; OnPropertyChanged(); }
        }

        private string _city;
        public string City
        {
            get => _city;
            set { _city = value; OnPropertyChanged(); }
        }

        private string _state;
        public string State
        {
            get => _state;
            set { _state = value; OnPropertyChanged(); }
        }

        private string _postalCode;
        public string PostalCode
        {
            get => _postalCode;
            set { _postalCode = value; OnPropertyChanged(); }
        }

        private string _country;
        public string Country
        {
            get => _country;
            set { _country = value; OnPropertyChanged(); }
        }

        private string _aadhaarNumber;
        public string AadhaarNumber
        {
            get => _aadhaarNumber;
            set { _aadhaarNumber = value; OnPropertyChanged(); }
        }

        private string _panNumber;
        public string PanNumber
        {
            get => _panNumber;
            set { _panNumber = value; OnPropertyChanged(); }
        }

        private string _profilePictureUrl;
        public string ProfilePictureUrl
        {
            get => _profilePictureUrl;
            set { _profilePictureUrl = value; OnPropertyChanged(); }
        }

        private string _timeZone;
        public string TimeZone
        {
            get => _timeZone;
            set { _timeZone = value; OnPropertyChanged(); }
        }

        private string _languagePreference;
        public string LanguagePreference
        {
            get => _languagePreference;
            set { _languagePreference = value; OnPropertyChanged(); }
        }


        public async Task LoadProfileAsync()
        {
            var email_id = await SecureStorage.GetAsync("user_email");
            var profile = await _profileService.GetUserProfileAsync(email_id);
            if (profile != null)
            {
                Id = profile.Id;
                Email = profile.Email;
                FirstName = profile.FirstName;
                LastName = profile.LastName;
                Gender = profile.Gender;
                DateOfBirth = profile.DateOfBirth;
                PhoneNumber = profile.PhoneNumber;
                AlternatePhoneNumber = profile.AlternatePhoneNumber;
                AddressLine1 = profile.AddressLine1;
                AddressLine2 = profile.AddressLine2;
                City = profile.City;
                State = profile.State;
                PostalCode = profile.PostalCode;
                Country = profile.Country;
                AadhaarNumber = profile.AadhaarNumber;
                PanNumber = profile.PANNumber;
                ProfilePictureUrl = profile.ProfilePictureUrl;
                TimeZone = profile.TimeZone;
                LanguagePreference = profile.LanguagePreference;
            }
        }

        private async Task SaveProfileAsync()
        {
            var profile = new UserProfileRequest()
            {
                //Id = Id,
                //Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Gender = Gender,
                //DateOfBirth = DateOfBirth,
                PhoneNumber = PhoneNumber,
                AlternatePhoneNumber = AlternatePhoneNumber,
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLine2,
                City = City,
                State = State,
                PostalCode = PostalCode,
                Country = Country,
                AadhaarNumber = AadhaarNumber,
                PANNumber = PanNumber,
                ProfilePictureUrl = ProfilePictureUrl,
                TimeZone = TimeZone,
                LanguagePreference = LanguagePreference
            };

            if (Id == Guid.Empty)
                await _profileService.CreateUserProfileAsync(profile);
            else
            {
                bool isUpdated = await _profileService.UpdateUserProfileAsync(Id.ToString(), profile);

                if (isUpdated)
                    await Shell.Current.GoToAsync("///HomePage");
                else
                    await _alertService.ShowAlertAsync("Error!", "Failed to save the Data!", "OK");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
