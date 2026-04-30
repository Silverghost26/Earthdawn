using Earthdawn.Data;

namespace Earthdawn.ViewModels;

public partial class CharacterCustomizationsViewModel : PageViewModel
{
    public string Test { get; set; } = "Welcome to the bound Character Customization for Earthdawn";
    public CharacterCustomizationsViewModel()
    {
        PageName = ApplicationPageNames.CharacterCustomizations;
    }
}