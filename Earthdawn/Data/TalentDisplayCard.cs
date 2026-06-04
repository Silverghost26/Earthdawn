using Earthdawn.Models;
namespace Earthdawn.Data;



public class TalentDisplayCard
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

    public Talent Talents
    {
        get => _talent ?? new Talent();
        set
        {
            _talent = value;
            _talent.Name = _name;
        }
    }
    private Talent? _talent;
}