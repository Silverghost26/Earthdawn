using Earthdawn.Data;

namespace Earthdawn.ViewModels;

public partial class DisciplinesViewModel : PageViewModel
{
    public string Test { get; set; } = "Welcome to the bound Disciplines for Earthdawn";
    public DisciplinesViewModel()
    {
        PageName = ApplicationPageNames.Disciplines;
    }
}