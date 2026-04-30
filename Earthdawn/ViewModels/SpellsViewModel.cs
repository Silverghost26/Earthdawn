using Earthdawn.Data;

namespace Earthdawn.ViewModels;

public partial class SpellsViewModel : PageViewModel
{
    public string Test { get; set; } = "Welcome to the bound Spells for Earthdawn";
    public SpellsViewModel()
    {
        PageName = ApplicationPageNames.Spells;
    }
}