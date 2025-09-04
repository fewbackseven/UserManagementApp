using System.Text.Json.Serialization;

namespace UserManagementApp.Models
{
    public class RegisterRequest
    {

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("userName")]
        public string? UserName { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

    }
}