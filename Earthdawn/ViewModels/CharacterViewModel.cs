using Earthdawn.Data;

namespace Earthdawn.ViewModels;

public partial class CharacterViewModel : PageViewModel
{
    public string Test { get; set; } = "Welcome to the bound Character for Earthdawn";
    public CharacterViewModel()
    {
        PageName = ApplicationPageNames.Character;
        
    }
}