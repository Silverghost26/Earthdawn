using System.Text.Json.Serialization;

namespace Earthdawn.Models;

public class SpiritPower
{
    [JsonPropertyName("Name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("Step")]
    public string Step { get; set; } = string.Empty;

    [JsonPropertyName("Action")]
    public string Action { get; set; } = string.Empty;

    [JsonPropertyName("Description")]
    public string Description { get; set; } = string.Empty;
}