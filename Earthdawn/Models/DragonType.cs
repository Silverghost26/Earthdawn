using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Earthdawn.Models;

public class DragonType : Creature
{
    [JsonPropertyName("attacks")]
    public Dictionary<string, string>? Attacks { get; set; }
    
    [JsonPropertyName("powerRanks")]
    public int PowerRanks { get; set; }
    
    [JsonPropertyName("specialManeuvers")]
    public List<string>? SpecialManeuversList { get; set; }
    
    [JsonPropertyName("culturalNotes")]
    public string? CulturalNotes { get; set; }
}