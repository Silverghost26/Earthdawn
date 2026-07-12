using EarthDawn.Models;

namespace Earthdawn.Data;

public class MountDisplayCard
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
    
    public Mount Mounts 
    {
        get => _mount ?? new Mount();
        set
        {
            _mount = value;
            _mount.Name = _name;
        }
    }
    private Mount? _mount;
}