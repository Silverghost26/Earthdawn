using Earthdawn.ViewModels;
using Earthdawn.Models;

namespace Earthdawn.Data;

public class EquipmentDisplayCard
{
    public string Name { get; set; } = string.Empty;
    public Equipment Equipment { get; set; } = new();
}