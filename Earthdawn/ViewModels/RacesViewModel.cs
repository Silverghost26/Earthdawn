using Earthdawn.Data;

namespace Earthdawn.ViewModels;

public partial class RacesViewModel : PageViewModel
{
    public string Test { get; set; } = "Welcome to the bound Races for Earthdawn";
    public RacesViewModel()
    {
        PageName = ApplicationPageNames.Races;
    }
}