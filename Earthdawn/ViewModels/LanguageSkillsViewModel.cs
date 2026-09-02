using CommunityToolkit.Mvvm.ComponentModel;
using Earthdawn.Data;

namespace Earthdawn.ViewModels;

public partial class LanguageSkillsViewModel : PageViewModel
{
    public LanguageSkillsViewModel()
    {
        PageName = ApplicationPageNames.SkillSelection;
    }
}