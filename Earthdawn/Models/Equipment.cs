using Earthdawn.ViewModels;

namespace Earthdawn.Models;

public class Equipment
{
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty; // Weapons, Armor, General, Mounts
    // Add more properties later
}

public class EquipmentDisplayCard : ViewModelBase
{
    private bool isSelected;
    public string Name { get; set; } = string.Empty;
    public Equipment Equipment { get; set; } = new();

    public bool IsSelected
    {
        get => isSelected;
        set => SetProperty(ref isSelected, value);
    }

    public string ButtonText => IsSelected ? "Remove" : "Add";
}