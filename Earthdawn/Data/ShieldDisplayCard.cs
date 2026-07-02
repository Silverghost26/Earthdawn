using Earthdawn.Models;
namespace Earthdawn.Data;

public class ShieldDisplayCard
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

    public Shield Shields
    {
        get => _shield ?? new Shield();
        set
        {
            _shield = value;
            _shield.Name = _name;
        }
    }
    private Shield? _shield;
}
