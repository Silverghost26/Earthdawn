using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    [ObservableProperty] private CharacterTalent? _selectedCharacterTalent;
    private Dictionary<string, Talent> CharacterTalents { get; }

    public CharacterCustomizationsViewModel(IDataServices dataServices, ICharacterSheetService characterSheetService)
    {
        _characterSheetService = characterSheetService;
        PageName = ApplicationPageNames.CharacterCustomizations;
        CharacterTalents = dataServices.LoadTalents();
        
        // Subscribe to property changes from the model
        _characterSheetService.CharacterCreationSheetInstance.PropertyChanged += OnCharacterSheetPropertyChanged;
    }

    private void OnCharacterSheetPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // Forward property change notifications to the view
        switch (e.PropertyName)
        {
            case nameof(CharacterCreationSheet.Dexterity):
                OnPropertyChanged(nameof(Dexterity));
                OnPropertyChanged(nameof(DexIncrementCost));
                OnPropertyChanged(nameof(DexDecrementCost));
                break;
            case nameof(CharacterCreationSheet.Strength):
                OnPropertyChanged(nameof(Strength));
                OnPropertyChanged(nameof(StrIncrementCost));
                OnPropertyChanged(nameof(StrDecrementCost));
                break;
            case nameof(CharacterCreationSheet.Toughness):
                OnPropertyChanged(nameof(Toughness));
                OnPropertyChanged(nameof(TouIncrementCost));
                OnPropertyChanged(nameof(TouDecrementCost));
                break;
            case nameof(CharacterCreationSheet.Perception):
                OnPropertyChanged(nameof(Perception));
                OnPropertyChanged(nameof(PerIncrementCost));
                OnPropertyChanged(nameof(PerDecrementCost));
                break;
            case nameof(CharacterCreationSheet.Willpower):
                OnPropertyChanged(nameof(Willpower));
                OnPropertyChanged(nameof(WilIncrementCost));
                OnPropertyChanged(nameof(WilDecrementCost));
                break;
            case nameof(CharacterCreationSheet.Charisma):
                OnPropertyChanged(nameof(Charisma));
                OnPropertyChanged(nameof(ChrIncrementCost));
                OnPropertyChanged(nameof(ChrDecrementCost));
                break;
            case nameof(CharacterCreationSheet.OriginalDex):
                OnPropertyChanged(nameof(OriginalDex));
                break;
            case nameof(CharacterCreationSheet.OriginalStr):
                OnPropertyChanged(nameof(OriginalStr));
                break;
            case nameof(CharacterCreationSheet.OriginalTou):
                OnPropertyChanged(nameof(OriginalTou));
                break;
            case nameof(CharacterCreationSheet.OriginalPer):
                OnPropertyChanged(nameof(OriginalPer));
                break;
            case nameof(CharacterCreationSheet.OriginalWil):
                OnPropertyChanged(nameof(OriginalWil));
                break;
            case nameof(CharacterCreationSheet.OriginalChr):
                OnPropertyChanged(nameof(OriginalChr));
                break;
            case nameof(CharacterCreationSheet.Initiative):
                OnPropertyChanged(nameof(Initiative));
                break;
            case nameof(CharacterCreationSheet.PhysicalDefense):
                OnPropertyChanged(nameof(PhysicalDefense));
                break;
            case nameof(CharacterCreationSheet.MysticDefense):
                OnPropertyChanged(nameof(MysticDefense));
                break;
            case nameof(CharacterCreationSheet.SocialDefense):
                OnPropertyChanged(nameof(SocialDefense));
                break;
            case nameof(CharacterCreationSheet.UnconsciousRating):
                OnPropertyChanged(nameof(UnconsciousRating));
                break;
            case nameof(CharacterCreationSheet.DeathRating):
                OnPropertyChanged(nameof(DeathRating));
                break;
            case nameof(CharacterCreationSheet.WoundThreshold):
                OnPropertyChanged(nameof(WoundThreshold));
                break;
            case nameof(CharacterCreationSheet.RecoveryTests):
                OnPropertyChanged(nameof(RecovertyTests));
                break;
            case nameof(CharacterCreationSheet.MysticalArmor):
                OnPropertyChanged(nameof(MysticalArmor));
                break;
            case nameof(CharacterCreationSheet.Karma):
                OnPropertyChanged(nameof(Karma));
                break;
            case nameof(CharacterCreationSheet.RemainingAttributePoints):
                OnPropertyChanged(nameof(RemainingAttributePoints));
                break;
        }
    }

    partial void OnTalentSelectedItemChanged(string? value)
    {
        if (value != null)
        {
            TalentSelectionChangedCommand.Execute(value);
        }
    }

    partial void OnSelectedCharacterTalentChanged(CharacterTalent? value)
    {
        if (value != null)
        {
            string talentName = value.TalentName;
            if (talentName.Contains("Thread Weaving"))
                talentName = "Thread Weaving";
            CurrentTalent = CharacterTalents[talentName];
        }
    }

    [RelayCommand]
    private void TalentSelectionChanged(string selectedItem)
    {
        if (selectedItem.Contains("Thread Weaving"))
            selectedItem = "Thread Weaving";
        CurrentTalent = CharacterTalents[selectedItem];
    }

    [RelayCommand]
    private void TalentIncreaseButtonClicked(CharacterTalent talent)
    {
        Debug.WriteLine($"Associated Talent: {talent.TalentName}");
        _characterSheetService.CharacterCreationSheetInstance.IncrementTalent(talent.TalentName);
    }

    [RelayCommand]
    private void TalentDecreaseButtonClicked(CharacterTalent talent)
    {
        Debug.WriteLine($"Associated Talent: {talent.TalentName}");
        _characterSheetService.CharacterCreationSheetInstance.DecremenetTalent(talent.TalentName);
    }
    
    [RelayCommand]
    private void SelectNoviceOptionTalent(string selectedTalent)
    {
        TalentSelectedItem = selectedTalent;
        SelectedOptionalTalent = selectedTalent;
    }

    [RelayCommand]
    private void IncrementAttribute(string selectedAttribute)
    {
        switch (selectedAttribute)
        {
            case "Dexterity":
                _characterSheetService.CharacterCreationSheetInstance.IncrementAttribute(AttributesTypes.Dex);
                break;
            case "Strength":
                _characterSheetService.CharacterCreationSheetInstance.IncrementAttribute(AttributesTypes.Str);
                break;
            case "Toughness":
                _characterSheetService.CharacterCreationSheetInstance.IncrementAttribute((AttributesTypes.Tou));
                break;
            case "Perception":
                _characterSheetService.CharacterCreationSheetInstance.IncrementAttribute((AttributesTypes.Per));
                break;
            case "Willpower":
                _characterSheetService.CharacterCreationSheetInstance.IncrementAttribute(AttributesTypes.Wil);
                break;
            case "Charisma":
                _characterSheetService.CharacterCreationSheetInstance.IncrementAttribute(AttributesTypes.Chr);
                break;
        }
    }

    [RelayCommand]
    private void DecrementAttribute(string selectedAttribute)
    {
        switch (selectedAttribute)
        {
            case "Dexterity":
                _characterSheetService.CharacterCreationSheetInstance.DecrementAttribute(AttributesTypes.Dex);
                break;
            case "Strength":
                _characterSheetService.CharacterCreationSheetInstance.DecrementAttribute(AttributesTypes.Str);
                break;
            case "Toughness":
                _characterSheetService.CharacterCreationSheetInstance.DecrementAttribute((AttributesTypes.Tou));
                break;
            case "Perception":
                _characterSheetService.CharacterCreationSheetInstance.DecrementAttribute((AttributesTypes.Per));
                break;
            case "Willpower":
                _characterSheetService.CharacterCreationSheetInstance.DecrementAttribute(AttributesTypes.Wil);
                break;
            case "Charisma":
                _characterSheetService.CharacterCreationSheetInstance.DecrementAttribute(AttributesTypes.Chr);
                break;
        }
    }

    [RelayCommand]
    private void IncrementTalent(string selectedTalent)
    {
        _characterSheetService.CharacterCreationSheetInstance.IncrementTalent(selectedTalent);
    }

    [RelayCommand]
    private void DecrementTalent(string selectedTalent)
    {
        _characterSheetService.CharacterCreationSheetInstance.DecremenetTalent(selectedTalent);
    }
    
    public int Dexterity => _characterSheetService.CharacterCreationSheetInstance.Dexterity;
    public int Strength => _characterSheetService.CharacterCreationSheetInstance.Strength;
    public int Toughness => _characterSheetService.CharacterCreationSheetInstance.Toughness;
    public int Perception => _characterSheetService.CharacterCreationSheetInstance.Perception;
    public int Willpower => _characterSheetService.CharacterCreationSheetInstance.Willpower;
    public int Charisma => _characterSheetService.CharacterCreationSheetInstance.Charisma;
    public int Karma => _characterSheetService.CharacterCreationSheetInstance.Karma;
    public int RacialKarma => _characterSheetService.CharacterCreationSheetInstance.KarmaModifier;
    public int PhysicalDefense => _characterSheetService.CharacterCreationSheetInstance.PhysicalDefense;
    public int MysticDefense => _characterSheetService.CharacterCreationSheetInstance.MysticDefense;
    public int PhysicalArmor => _characterSheetService.CharacterCreationSheetInstance.PhysicalArmor;
    public int MysticalArmor => _characterSheetService.CharacterCreationSheetInstance.MysticalArmor;
    public int SocialDefense => _characterSheetService.CharacterCreationSheetInstance.SocialDefense;
    public int Initiative => _characterSheetService.CharacterCreationSheetInstance.Initiative;
    public int UnconsciousRating => _characterSheetService.CharacterCreationSheetInstance.UnconsciousRating;
    public int DeathRating => _characterSheetService.CharacterCreationSheetInstance.DeathRating;
    public int WoundThreshold => _characterSheetService.CharacterCreationSheetInstance.WoundThreshold;
    public int RecovertyTests => _characterSheetService.CharacterCreationSheetInstance.RecoveryTests;
    public int OriginalDex => _characterSheetService.CharacterCreationSheetInstance.OriginalDex;
    public int OriginalStr => _characterSheetService.CharacterCreationSheetInstance.OriginalStr;
    public int OriginalTou => _characterSheetService.CharacterCreationSheetInstance.OriginalTou;
    public int OriginalPer => _characterSheetService.CharacterCreationSheetInstance.OriginalPer;
    public int OriginalWil => _characterSheetService.CharacterCreationSheetInstance.OriginalWil;
    public int OriginalChr => _characterSheetService.CharacterCreationSheetInstance.OriginalChr;
    public int DexIncrementCost => _characterSheetService.CharacterCreationSheetInstance.GetAttributeIncreaseCostDex();
    public int StrIncrementCost => _characterSheetService.CharacterCreationSheetInstance.GetAttributeIncreaseCostStr();
    public int TouIncrementCost => _characterSheetService.CharacterCreationSheetInstance.GetAttributeIncreaseCostTou();
    public int PerIncrementCost => _characterSheetService.CharacterCreationSheetInstance.GetAttributeIncreaseCostPer();
    public int WilIncrementCost => _characterSheetService.CharacterCreationSheetInstance.GetAttributeIncreaseCostWil();
    public int ChrIncrementCost => _characterSheetService.CharacterCreationSheetInstance.GetAttributeIncreaseCostChr();
    public int DexDecrementCost => _characterSheetService.CharacterCreationSheetInstance.GetAttributeDecrementCostDex();
    public int StrDecrementCost => _characterSheetService.CharacterCreationSheetInstance.GetAttributeDecrementCostStr();
    public int TouDecrementCost => _characterSheetService.CharacterCreationSheetInstance.GetAttributeDecrementCostTou();
    public int PerDecrementCost => _characterSheetService.CharacterCreationSheetInstance.GetAttributeDecrementCostPer();
    public int WilDecrementCost => _characterSheetService.CharacterCreationSheetInstance.GetAttributeDecrementCostWil();
    public int ChrDecrementCost => _characterSheetService.CharacterCreationSheetInstance.GetAttributeDecrementCostChr();
    

    public int RemainingAttributePoints =>
        _characterSheetService.CharacterCreationSheetInstance.RemainingAttributePoints;
    
    public List<string> Talents =>
        _characterSheetService.CharacterCreationSheetInstance.GetTalentNameList();

    public List<string> NoviceOptionTalents =>
        _characterSheetService.CharacterCreationSheetInstance.GetOptionalTalents();

    public List<CharacterTalent> DisciplineTalents =>
        _characterSheetService.CharacterCreationSheetInstance.GetDisciplineTalentList();

    public List<CharacterTalent> FreeDisciplineTalents =>
        _characterSheetService.CharacterCreationSheetInstance.GetFreeTalentList();

}