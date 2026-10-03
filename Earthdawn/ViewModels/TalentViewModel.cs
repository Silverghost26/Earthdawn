
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Models;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public partial class TalentViewModel : ObservableObject
{
    private readonly Talent _talent;

    public TalentViewModel(Talent talent)
    {
        
        _talent = talent;
        _rank = talent.Rank;
        _name = talent.Name;
        _step = talent.Rank;
    }
    [ObservableProperty] private Talent? _selectedCharacterTalent;
    
    public int Strain => _talent.Strain;
    public string Action => _talent.Action;
    public string SkillUse => _talent.SkillUse;
    public string SkillLevel => _talent.SkillLevel;
    public string Description => _talent.Description;
    public int CircleObtained => _talent.CircleObtained;

    [ObservableProperty]
    private int _rank;

    [ObservableProperty] private string _name;
    [ObservableProperty] private int _step;
}
