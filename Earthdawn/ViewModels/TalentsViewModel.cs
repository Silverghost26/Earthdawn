using Earthdawn.Data;

namespace Earthdawn.ViewModels;

public partial class TalentsViewModel : PageViewModel
{
    public string Test { get; set; } = "Welcome to the bound Talents for Earthdawn";
    public TalentsViewModel()
    {
        PageName = ApplicationPageNames.Talents;
    }
}