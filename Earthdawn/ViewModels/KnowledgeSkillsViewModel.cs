using CommunityToolkit.Mvvm.ComponentModel;
using Earthdawn.Data;

namespace Earthdawn.ViewModels;

public partial class KnowledgeSkillsViewModel : PageViewModel
{
    public KnowledgeSkillsViewModel()
    {
        PageName = ApplicationPageNames.SkillSelection;
    }
}