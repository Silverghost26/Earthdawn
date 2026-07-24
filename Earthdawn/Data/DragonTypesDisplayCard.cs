using System.Collections.Generic;
using System.Text.Json.Serialization;
using Earthdawn.Models;

namespace Earthdawn.Data;

public class DragonTypesContainer
{
    [JsonPropertyName("dragonTypes")]
    public List<DragonType>? DragonTypes { get; set; }
}
