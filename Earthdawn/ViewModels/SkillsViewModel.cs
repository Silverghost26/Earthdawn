using Earthdawn.Data;

namespace Earthdawn.ViewModels;

public class SkillsViewModel : PageViewModel
{
    public string Test { get; set; } = "Welcome to the bound Skills for Earthdawn";
    public SkillsViewModel()
    {
        PageName = ApplicationPageNames.Skills;
    }
}