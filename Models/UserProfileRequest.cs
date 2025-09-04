namespace UserManagementApp.Models
{
    public class UserProfileRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }
        public string AlternatePhoneNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public string AadhaarNumber { get; set; }
        public string PANNumber { get; set; }

        public string ProfilePictureUrl { get; set; }
        public string TimeZone { get; set; }
        public string LanguagePreference { get; set; }
    }
}
