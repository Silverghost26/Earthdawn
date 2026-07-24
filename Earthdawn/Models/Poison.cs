using System.Text.Json.Serialization;

namespace Earthdawn.Models;

public class Poison
{
    [JsonPropertyName("Name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("Type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("OnsetTime")]
    public string OnsetTime { get; set; } = string.Empty;

    [JsonPropertyName("Duration")]
    public string Duration { get; set; } = string.Empty;

    [JsonPropertyName("EffectStep")]
    public int EffectStep { get; set; }

    [JsonPropertyName("Interval")]
    public string Interval { get; set; } = string.Empty;

    [JsonPropertyName("Description")]
    public string Description { get; set; } = string.Empty;
}

public class PoisonsContainer : System.Collections.Generic.Dictionary<string, Poison>
{
    // This class inherits from Dictionary<string, Poison> to match the JSON structure
    // The keys are poison names and values are Poison objects
}