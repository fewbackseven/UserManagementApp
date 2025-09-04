using System.Text.Json.Serialization;

namespace UserManagementApp.Models
{
    public class AuthResponse
    {

        [JsonPropertyName("accessToken")]
        public string? AccessToken { get; set; }

        [JsonPropertyName("expiresAtUtc")]
        public DateTime ExpiresAtUtc { get; set; }

        [JsonPropertyName("refreshToken")]
        public string? RefreshToken { get; set; }

        [JsonPropertyName("refreshExpiresAtUtc")]
        public DateTime? RefreshExpiresAtUtc { get; set; }

    }
}
