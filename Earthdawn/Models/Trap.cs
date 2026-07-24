using System.Text.Json.Serialization;

namespace Earthdawn.Models;

public class Trap
{
    [JsonPropertyName("Name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("Detection")]
    public int Detection { get; set; }

    [JsonPropertyName("Disarm")]
    public string Disarm { get; set; } = string.Empty;

    [JsonPropertyName("Initiative")]
    public string Initiative { get; set; } = string.Empty;

    [JsonPropertyName("Trigger")]
    public string Trigger { get; set; } = string.Empty;

    [JsonPropertyName("Effect")]
    public string Effect { get; set; } = string.Empty;

    [JsonPropertyName("Type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("OnsetTime")]
    public string OnsetTime { get; set; } = string.Empty;

    [JsonPropertyName("Duration")]
    public string Duration { get; set; } = string.Empty;

    [JsonPropertyName("StepNumber")]
    public int StepNumber { get; set; }

    [JsonPropertyName("Interval")]
    public string Interval { get; set; } = string.Empty;

    [JsonPropertyName("Description")]
    public string Description { get; set; } = string.Empty;
}

public class TrapsContainer : System.Collections.Generic.Dictionary<string, Trap>
{
    // This class inherits from Dictionary<string, Trap> to match the JSON structure
    // The keys are trap names and values are Trap objects
}