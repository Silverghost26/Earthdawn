using System.Text.Json.Serialization;

namespace Earthdawn.Models
{
    public class TradeService
    {
        [JsonPropertyName("Service")]
        public string Service { get; set; } = string.Empty;

        [JsonPropertyName("Cost")]
        public string Cost { get; set; } = string.Empty;

        [JsonPropertyName("Availability")]
        public string Availability { get; set; } = string.Empty;
    }
}
