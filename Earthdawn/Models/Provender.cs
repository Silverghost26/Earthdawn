using System.Text.Json.Serialization;

namespace Earthdawn.Models
{
    public class Provender
    {
        [JsonPropertyName("Item")]
        public string Item { get; set; } = string.Empty;

        [JsonPropertyName("Cost")]
        public string Cost { get; set; } = string.Empty;

        [JsonPropertyName("Weight")]
        public string Weight { get; set; } = string.Empty;

        [JsonPropertyName("Availability")]
        public string Availability { get; set; } = string.Empty;
    }
}
