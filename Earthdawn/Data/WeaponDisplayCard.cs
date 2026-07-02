using Earthdawn.Models;
using EarthDawn.Models;

namespace Earthdawn.Data;

public class WeaponDisplayCard
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

    public Weapon Weapons
    {
        get => _weapon ?? new Weapon();
        set
        {
            _weapon = value;
            _weapon.WeaponName = _name;
        }
    }
    private Weapon? _weapon;
}
