using Earthdawn.Data;

namespace Earthdawn.ViewModels;

public partial class HomeViewModel : PageViewModel
{
    public string Test { get; set; } = "Welcome to the bound Home for Earthdawn";
    public HomeViewModel()
    {
        PageName = ApplicationPageNames.Home;
    }
}