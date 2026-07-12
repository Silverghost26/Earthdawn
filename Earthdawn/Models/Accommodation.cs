using System.Text.Json.Serialization;

namespace Earthdawn.Models
{
    public class Accommodation
    {
        [JsonPropertyName("Item")]
        public string Item { get; set; } = string.Empty;

        [JsonPropertyName("Cost Per Night")]
        public string CostPerNight { get; set; } = string.Empty;

        [JsonPropertyName("Availability")]
        public string Availability { get; set; } = string.Empty;
    }
}
