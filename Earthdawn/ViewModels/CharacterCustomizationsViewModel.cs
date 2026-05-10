using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using Earthdawn.Models;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public partial class CharacterCustomizationsViewModel : PageViewModel
{
    
    private readonly ICharacterSheetService _characterSheetService;

    [ObservableProperty] private string? _talentSelectedItem;
    [ObservableProperty] private string? _talentButtonItem;
    [ObservableProperty] private string? _selectedOptionalTalent;
    [ObservableProperty] private Talent? _currentTalent;
    private Dictionary<string, Talent> CharacterTalents { get; }

    public CharacterCustomizationsViewModel(IDataServices dataServices, ICharacterSheetService characterSheetService)
    {
        _characterSheetService = characterSheetService;
        PageName = ApplicationPageNames.CharacterCustomizations;
        CharacterTalents = dataServices.LoadTalents();
    }

    partial void OnTalentSelectedItemChanged(string? value)
    {
        if (value != null)
        {
            TalentSelectionChangedCommand.Execute(value);
        }
    }

    [RelayCommand]
    private void TalentSelectionChanged(string selectedItem)
    {
        Debug.WriteLine($"Selected: {selectedItem}");
        if (selectedItem.Contains("Thread Weaving"))
            selectedItem = "Thread Weaving";
        CurrentTalent = CharacterTalents[selectedItem];
    }

    [RelayCommand]
    private void TalentIncreaseButtonClicked(string talentButtonItem)
    {
        Debug.WriteLine($"Associated Talent: {talentButtonItem}");
    }
    
    [RelayCommand]
    private void SelectNoviceOptionTalent(string selectedTalent)
    {
        TalentSelectedItem = selectedTalent;
        SelectedOptionalTalent = selectedTalent;
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

    public List<string> NoviceOptionTalents =>
        _characterSheetService.CharacterSheetInstance.CharacterDiscipline.TalentOptions["Novice"];
}