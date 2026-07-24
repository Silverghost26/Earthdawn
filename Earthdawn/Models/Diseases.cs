using System.Text.Json.Serialization;

namespace Earthdawn.Models;

public class Disease
{
    [JsonPropertyName("Name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("Type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("OnsetTime")]
    public string OnsetTime { get; set; } = string.Empty;

    [JsonPropertyName("EffectStep")]
    public int EffectStep { get; set; }

    [JsonPropertyName("Interval")]
    public string Interval { get; set; } = string.Empty;

    [JsonPropertyName("Duration")]
    public string Duration { get; set; } = string.Empty;

    [JsonPropertyName("Description")]
    public string Description { get; set; } = string.Empty;
}

public class DiseasesContainer : System.Collections.Generic.Dictionary<string, Disease>
{
    // This class inherits from Dictionary<string, Disease> to match the JSON structure
    // The keys are disease names and values are Disease objects
}