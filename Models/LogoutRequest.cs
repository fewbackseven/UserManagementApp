using System.Text.Json.Serialization;

namespace UserManagementApp.Models
{
    public class LogoutRequest
    {
        [JsonPropertyName("refreshToken")]
        public string? RefreshToken { get; set; }
    }

}
