using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using Earthdawn.Models;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public partial class CharacterCustomizationsViewModel : PageViewModel
{
    
    private readonly ICharacterSheetService _characterSheetService;
    
    public CharacterCustomizationsViewModel(ICharacterSheetService characterSheetService)
    {
        _characterSheetService = characterSheetService;
        PageName = ApplicationPageNames.CharacterCustomizations;
    }

    public int Dexterity => _characterSheetService.CharacterSheetInstance.CharAttributes.Dexterity;
    public int Strength => _characterSheetService.CharacterSheetInstance.CharAttributes.Strength;
    public int Toughness => _characterSheetService.CharacterSheetInstance.CharAttributes.Toughness;
    public int Perception => _characterSheetService.CharacterSheetInstance.CharAttributes.Perception;
    public int Willpower => _characterSheetService.CharacterSheetInstance.CharAttributes.Willpower;
    public int Charisma => _characterSheetService.CharacterSheetInstance.CharAttributes.Charisma;
    public int Karma => _characterSheetService.CharacterSheetInstance.Karma;

    public List<string> Talents =>
        _characterSheetService.CharacterSheetInstance.CharacterDiscipline.Circles["First"].Talents;

}