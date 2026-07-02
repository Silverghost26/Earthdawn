using Earthdawn.Models;
namespace Earthdawn.Data;

public class SkillDisplayCard
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

    public Skill Skills
    {
        get => _skill ?? new Skill();
        set
        {
            _skill = value;
            _skill.Name = _name;
        }
    }
    private Skill? _skill;
}
