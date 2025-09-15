using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UserManagementApp.Models
{
    public class LoginRequest
    {
        [JsonPropertyName("email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]       
        public string? Email { get; set; }

        [JsonPropertyName("password")]
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string? Password { get; set; }

    }
}
