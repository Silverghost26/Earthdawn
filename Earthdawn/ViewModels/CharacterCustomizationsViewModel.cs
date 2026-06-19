using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using Earthdawn.Models;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public partial class CharacterCustomizationsViewModel : PageViewModel
{
    private readonly ICharacterSheetService _characterSheetService;
    private Talent _currentlySelectedOptionalTalent;
    
    [ObservableProperty] private string? _talentSelectedItem;
    [ObservableProperty] private string? _talentButtonItem;
    [ObservableProperty] private string? _selectedOptionalTalent;
    [ObservableProperty] private Talent? _currentTalent;
    //[ObservableProperty] private Talent? _selectedCharacterTalent;
    [ObservableProperty] private TalentViewModel? _selectedCharacterTalent;
    [ObservableProperty] private int _dexterity;
    [ObservableProperty] private int _strength;
    [ObservableProperty] private int _toughness;
    [ObservableProperty] private int _perception;
    [ObservableProperty] private int _willpower;
    [ObservableProperty] private int _charisma;
    [ObservableProperty] private int _dexIncrementCost;
    [ObservableProperty] private int _dexDecrementCost;
    [ObservableProperty] private int _strIncrementCost;
    [ObservableProperty] private int _strDecrementCost;
    [ObservableProperty] private int _perIncrementCost;
    [ObservableProperty] private int _perDecrementCost;
    [ObservableProperty] private int _wilIncrementCost;
    [ObservableProperty] private int _wilDecrementCost;
    [ObservableProperty] private int _touIncrementCost;
    [ObservableProperty] private int _touDecrementCost;
    [ObservableProperty] private int _chrIncrementCost;
    [ObservableProperty] private int _chrDecrementCost;

    [ObservableProperty] private int _initiative;
    [ObservableProperty] private int _physicalDefense;
    [ObservableProperty] private int _mysticDefense;
    [ObservableProperty] private int _socialDefense;
    [ObservableProperty] private int _mysticalArmor;
    [ObservableProperty] private int _karma;
    [ObservableProperty] private int _unconsciousRating;
    [ObservableProperty] private int _deathRating;
    [ObservableProperty] private int _woundThreshold;
    [ObservableProperty] private int _recoveryTests;
    [ObservableProperty] private int _remainingAttributePoints;
    [ObservableProperty] private int _remainingTalentPoints;
    [ObservableProperty] private int _optionalTalentRank;
    [ObservableProperty] private int _optionalTalentStep;

     public ObservableCollection<string> OptionalTalents { get; }
     public ObservableCollection<TalentViewModel> DisciplineTalents { get; }
     public ObservableCollection<TalentViewModel> FreeTalents { get; }
    
    
    private Dictionary<string, Talent> CharacterTalents { get; }

    public CharacterCustomizationsViewModel(IDataServices dataServices, ICharacterSheetService characterSheetService)
    {
        _characterSheetService = characterSheetService;
        foreach (DisciplineDisplayCard ddc in dataServices.LoadDisciplines())
        {
            if (_characterSheetService.CharacterCreationSheetInstance.GetDiscipline()[0].DisciplineName == ddc.Name)
            {
                OptionalTalents = new ObservableCollection<string>(ddc.Disciplines.TalentOptions["Novice"]);
            }
        }
        
        PageName = ApplicationPageNames.CharacterCustomizations;
        CharacterTalents = dataServices.LoadTalents();
        UpdateAttributeValues("Dexterity");
        UpdateAttributeValues("Strength");
        UpdateAttributeValues("Toughness");
        UpdateAttributeValues("Perception");
        UpdateAttributeValues("Willpower");
        UpdateAttributeValues("Charisma");
        Karma = _characterSheetService.CharacterCreationSheetInstance.Karma;
        Initiative = _characterSheetService.CharacterCreationSheetInstance.Initiative;
        PhysicalDefense = _characterSheetService.CharacterCreationSheetInstance.PhysicalDefense;
        MysticDefense = _characterSheetService.CharacterCreationSheetInstance.MysticDefense;
        SocialDefense = _characterSheetService.CharacterCreationSheetInstance.SocialDefense;
        MysticalArmor = _characterSheetService.CharacterCreationSheetInstance.MysticalArmor;
        Karma = _characterSheetService.CharacterCreationSheetInstance.Karma;
        UnconsciousRating = _characterSheetService.CharacterCreationSheetInstance.UnconsciousRating;
        DeathRating = _characterSheetService.CharacterCreationSheetInstance.DeathRating;
        WoundThreshold = _characterSheetService.CharacterCreationSheetInstance.WoundThreshold;
        RecoveryTests = _characterSheetService.CharacterCreationSheetInstance.RecoveryTests;
        RemainingAttributePoints = _characterSheetService.CharacterCreationSheetInstance.RemainingAttributePoints;
        
        DisciplineTalents =
            new ObservableCollection<TalentViewModel>(_characterSheetService.CharacterCreationSheetInstance.GetDiscipline()[0]
                .GetDisciplineTalents().Select(t => new TalentViewModel(t)));
        FreeTalents = new ObservableCollection<TalentViewModel>(
            _characterSheetService.CharacterCreationSheetInstance.GetDiscipline()[0].GetDisciplineFreeTalents().Select(t => new TalentViewModel(t)));
    }
    
    partial void OnTalentSelectedItemChanged(string? value)
    {
        if (value != null)
        {
            TalentSelectionChangedCommand.Execute(value);
        }
    }
    
    partial void OnSelectedCharacterTalentChanged(TalentViewModel? value)
    {
        if (value != null)
        {
            string talentName = value.Name;
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
    private void TalentIncreaseButtonClicked(TalentViewModel talent)
    {
        IncrementTalent(talent.Name);
    }

    [RelayCommand]
    private void TalentDecreaseButtonClicked(TalentViewModel talent)
    {
        DecrementTalent(talent.Name);
    }

    [RelayCommand]
    private void OptionalTalentDecreaseButtonClicked(string selectedTalent)
    {
        DecrementTalent(selectedTalent);
    }
    
    [RelayCommand]
    private void OptionalTalentIncrementButtonClicked(string selectedTalent)
    {
        IncrementTalent(selectedTalent);
    }
    
    [RelayCommand]
    private void IncrementTalent(string selectedTalent)
    {
        if (string.IsNullOrWhiteSpace(selectedTalent))
            return;
        _characterSheetService.CharacterCreationSheetInstance.IncrementTalent(selectedTalent);
        RemainingTalentPoints = _characterSheetService.CharacterCreationSheetInstance.RemainingTalentPoints;
        UpdateAllTalents();
    }

    [RelayCommand]
    private void DecrementTalent(string selectedTalent)
    {
        if (string.IsNullOrWhiteSpace(selectedTalent))
            return;
        _characterSheetService.CharacterCreationSheetInstance.DecrementTalent(selectedTalent);
        RemainingTalentPoints = _characterSheetService.CharacterCreationSheetInstance.RemainingTalentPoints;
        UpdateAllTalents();
    }

    [RelayCommand]
    private void SelectNoviceOptionTalent(string selectedTalent)
    {
        if (selectedTalent != SelectedOptionalTalent)
        {
            if (_currentlySelectedOptionalTalent != null && _currentlySelectedOptionalTalent.Name == selectedTalent)
            {
                TalentSelectedItem = selectedTalent;
                SelectedOptionalTalent = selectedTalent;
            }
            else
            {
                _currentlySelectedOptionalTalent = new Talent(CharacterTalents[selectedTalent]);
                _currentlySelectedOptionalTalent.Name = selectedTalent;
                if (string.IsNullOrEmpty(SelectedOptionalTalent))
                {
                        _characterSheetService.CharacterCreationSheetInstance.AddOptionalTalent(
                            _currentlySelectedOptionalTalent);
                }
                else
                {
                        _characterSheetService.CharacterCreationSheetInstance.AddOptionalTalent(
                            _currentlySelectedOptionalTalent, SelectedOptionalTalent);
                }
                TalentSelectedItem = selectedTalent;
                SelectedOptionalTalent = selectedTalent;
            }
            RemainingTalentPoints = _characterSheetService.CharacterCreationSheetInstance.RemainingTalentPoints;
        }
    }

    private void UpdateAllTalents()
    {
        foreach (Talent talent in _characterSheetService.CharacterCreationSheetInstance.GetDiscipline()[0]
                     .GetDisciplineTalents())
        {
            foreach (TalentViewModel dt in DisciplineTalents)
            {
                if (talent.Name == dt.Name)
                {
                    dt.Rank = talent.Rank;
                    break;
                }
            }
        }

        if (_currentlySelectedOptionalTalent != null
            && _characterSheetService.CharacterCreationSheetInstance.GetDiscipline()[0]
                .GetDisciplineOptionalTalents() != null)
        {
            _currentlySelectedOptionalTalent.Rank = _characterSheetService.CharacterCreationSheetInstance.GetDiscipline()[0]
                .GetDisciplineOptionalTalents()[0].Rank;
        }
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
        UpdateAttributeValues(selectedAttribute);
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
        UpdateAttributeValues(selectedAttribute);
    }

    private void UpdateAttributeValues(string attribute)
    {
        switch (attribute)
        {
            case "Dexterity":
                Dexterity = _characterSheetService.CharacterCreationSheetInstance.Dexterity;
                DexIncrementCost = _characterSheetService.CharacterCreationSheetInstance.GetAttributeIncreaseCostDex();
                DexDecrementCost = _characterSheetService.CharacterCreationSheetInstance.GetAttributeDecrementCostDex();
                Initiative = _characterSheetService.CharacterCreationSheetInstance.Initiative;
                PhysicalDefense = _characterSheetService.CharacterCreationSheetInstance.PhysicalDefense;
                break;
            case "Strength":
                Strength = _characterSheetService.CharacterCreationSheetInstance.Strength;
                StrIncrementCost = _characterSheetService.CharacterCreationSheetInstance.GetAttributeIncreaseCostStr();
                StrDecrementCost = _characterSheetService.CharacterCreationSheetInstance.GetAttributeDecrementCostStr();
                break;
            case "Toughness":
                Toughness = _characterSheetService.CharacterCreationSheetInstance.Toughness;
                TouIncrementCost = _characterSheetService.CharacterCreationSheetInstance.GetAttributeIncreaseCostTou();
                TouDecrementCost = _characterSheetService.CharacterCreationSheetInstance.GetAttributeDecrementCostTou();
                UnconsciousRating = _characterSheetService.CharacterCreationSheetInstance.UnconsciousRating;
                DeathRating = _characterSheetService.CharacterCreationSheetInstance.DeathRating;
                WoundThreshold = _characterSheetService.CharacterCreationSheetInstance.WoundThreshold;
                RecoveryTests = _characterSheetService.CharacterCreationSheetInstance.RecoveryTests;
                break;
            case "Perception":
                Perception = _characterSheetService.CharacterCreationSheetInstance.Perception;
                PerIncrementCost = _characterSheetService.CharacterCreationSheetInstance.GetAttributeIncreaseCostPer();
                PerDecrementCost = _characterSheetService.CharacterCreationSheetInstance.GetAttributeDecrementCostPer();
                MysticDefense = _characterSheetService.CharacterCreationSheetInstance.MysticDefense;
                break;
            case "Willpower":
                Willpower = _characterSheetService.CharacterCreationSheetInstance.Willpower;
                WilIncrementCost = _characterSheetService.CharacterCreationSheetInstance.GetAttributeIncreaseCostWil();
                WilDecrementCost = _characterSheetService.CharacterCreationSheetInstance.GetAttributeDecrementCostWil();
                MysticalArmor = _characterSheetService.CharacterCreationSheetInstance.MysticalArmor;
                break;
            case "Charisma":
                Charisma = _characterSheetService.CharacterCreationSheetInstance.Charisma;
                ChrIncrementCost = _characterSheetService.CharacterCreationSheetInstance.GetAttributeIncreaseCostChr();
                ChrDecrementCost = _characterSheetService.CharacterCreationSheetInstance.GetAttributeDecrementCostChr();
                SocialDefense = _characterSheetService.CharacterCreationSheetInstance.SocialDefense;
                break;
        }
        RemainingAttributePoints = _characterSheetService.CharacterCreationSheetInstance.RemainingAttributePoints;
        Karma = _characterSheetService.CharacterCreationSheetInstance.Karma;
    }
    
    public int RacialKarma => _characterSheetService.CharacterCreationSheetInstance.KarmaModifier;


}