using System.Text.Json.Serialization;

namespace UserManagementApp.Models
{
    public class LogoutRequest
    {
        [JsonPropertyName("refreshToken")]
        public string? RefreshToken { get; set; }
    }

    public class Category
    {
        public string Name { get; set; }
    }

    public class FoodItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }
        public string ImageUrl { get; set; }
        public int DeliveryTime { get; set; }
        public double DistanceKm { get; set; }
        public decimal? OfferPrice { get; set; }
        public List<string> Tags { get; set; }
        public string DeliveryInfo =>
            $"{DeliveryTime}–{DeliveryTime + 5} mins • {DistanceKm:F1} km";
    }


}
