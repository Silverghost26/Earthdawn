using Earthdawn.Models;
namespace Earthdawn.Data;

public class ArmorDisplayCard
{
    public string Name
    {
        get => _name ?? string.Empty;
        set
        {
            _name = value;
        }
    }
    private string? _name;

    public Armor Armors
    {
        get => _armor ?? new Armor();
        set
        {
            _armor = value;
            _armor.Name = _name;
        }
    }
    private Armor? _armor;
}
