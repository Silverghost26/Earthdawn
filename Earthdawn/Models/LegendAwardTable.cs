using System.Text.Json.Serialization;

namespace Earthdawn.Models;

public class LegendAwardEntry
{
    [JsonPropertyName("Circle")]
    public int Circle { get; set; }

    [JsonPropertyName("LegendAward")]
    public string LegendAward { get; set; } = string.Empty;

    [JsonPropertyName("TotalLegendPerSession")]
    public string TotalLegendPerSession { get; set; } = string.Empty;
}

public class LegendAwardTable : System.Collections.Generic.Dictionary<string, LegendAwardEntry>
{
    // This class inherits from Dictionary<string, LegendAwardEntry> to match the JSON structure
    // The keys are circle numbers (as strings) and values are LegendAwardEntry objects
}