using System.Text.Json.Serialization;

namespace UserManagementApp.Models
{

    public class RefreshRequest
    {
        [JsonPropertyName("refreshToken")]
        public string? RefreshToken { get; set; }
    }

}
