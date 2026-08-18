using System.Text.Json.Serialization;

namespace Earthdawn.Models
{
    public class Clothing : EquipmentBase
    {
        [JsonPropertyName("Item")]
        public string Item { get; set; } = string.Empty;
    }
}
