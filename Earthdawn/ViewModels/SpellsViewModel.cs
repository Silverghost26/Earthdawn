using System;
// using System.Collections.ObjectCollection;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using Earthdawn.Models;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public partial class SpellsViewModel : PageViewModel
{
    [ObservableProperty] private int _currentIndex;
    [ObservableProperty] private string _disciplineName;
    
    // New properties for spell circles with navigation support
    [ObservableProperty] private SpellCollectionViewModel _circleOneSpells = new();
    [ObservableProperty] private SpellCollectionViewModel _circleTwoSpells = new();
    
    // Current indices for each carousel
    [ObservableProperty] private int _circleOneCurrentIndex;
    [ObservableProperty] private int _circleTwoCurrentIndex;
    
    [ObservableProperty] private int _spellPointsRemaining;
    
    // Properties to track button text for select/remove functionality
    [ObservableProperty] private string _circleOneButtonText = "Select";
    [ObservableProperty] private string _circleTwoButtonText = "Select";

    public ObservableCollection<SpellDisplayCard> Spells { get; }
    private ICharacterSheetService _characterSheetService;
    private IDataServices _dataServices;

    public SpellsViewModel(IDataServices dataServices, ICharacterSheetService characterSheetService)
    {
        _characterSheetService = characterSheetService;
        _dataServices = dataServices;
        PageName = ApplicationPageNames.Spells;
        Spells = new ObservableCollection<SpellDisplayCard>(dataServices.LoadSpells());
        DisciplineName = _characterSheetService.CharacterCreationSheetInstance.GetDiscipline()[0].DisciplineName;
        SpellPointsRemaining = _characterSheetService.CharacterCreationSheetInstance.SpellPoints;
        
        // Initialize with first discipline
        UpdateSpellsForDiscipline();
        // Initialize Spell Select button
        UpdateCircleOneButtonText();
        UpdateCircleTwoButtonText();
    }

    partial void OnDisciplineNameChanged(string value)
    {
        UpdateSpellsForDiscipline();
    }

    private void UpdateSpellsForDiscipline()
    {
        foreach (var spellCard in Spells)
        {
            if (DisciplineName == spellCard.Name)
            {
                CircleOneSpells = new SpellCollectionViewModel(spellCard.Book.Circle_1);
                CircleTwoSpells = new SpellCollectionViewModel(spellCard.Book.Circle_2);
            }
            CircleOneCurrentIndex = 0;
            CircleTwoCurrentIndex = 0;
        }
    }

    // Helper method to check if a spell is already selected
    private bool IsSpellSelected(Spell spell)
    {
        var selectedSpells = _characterSheetService.CharacterCreationSheetInstance
            .GetDiscipline()[0]
            .GetSpellBook()
            .Spells;
        if (selectedSpells.Any(item => item.Name == spell.Name))
        {
            return true;
        }
        return false;
    }

    // Update button text based on current spell selection
    private void UpdateCircleOneButtonText()
    {
        if (CircleOneSpells.Spells.Count == 0)
        {
            CircleOneButtonText = "Select";
            return;
        }
        
        var currentSpell = CircleOneSpells.Spells[CircleOneCurrentIndex];
        CircleOneButtonText = IsSpellSelected(currentSpell) ? "Remove" : "Select";
    }

    private void UpdateCircleTwoButtonText()
    {
        if (CircleTwoSpells.Spells.Count == 0)
        {
            CircleTwoButtonText = "Select";
            return;
        }
        
        var currentSpell = CircleTwoSpells.Spells[CircleTwoCurrentIndex];
        CircleTwoButtonText = IsSpellSelected(currentSpell) ? "Remove" : "Select";
    }

    [RelayCommand]
    private void PreviousCircleOne()
    {
        if (CircleOneSpells.Spells.Count == 0) return;
        
        CircleOneCurrentIndex--;
        if (CircleOneCurrentIndex < 0)
        {
            CircleOneCurrentIndex = CircleOneSpells.Spells.Count - 1; // Wrap to end
        }
        
        // Update button text when selection changes
        UpdateCircleOneButtonText();
    }

    [RelayCommand]
    private void NextCircleOne()
    {
        if (CircleOneSpells.Spells.Count == 0) return;
        
        CircleOneCurrentIndex++;
        if (CircleOneCurrentIndex >= CircleOneSpells.Spells.Count)
        {
            CircleOneCurrentIndex = 0; // Wrap to beginning
        }
        
        // Update button text when selection changes
        UpdateCircleOneButtonText();
    }

    [RelayCommand]
    private void SelectCircleOne()
    {
        if (CircleOneSpells.Spells.Count == 0) return;
        
        var currentSpell = CircleOneSpells.Spells[CircleOneCurrentIndex];
        
        if (IsSpellSelected(currentSpell))
        {
            // Update the button Text
            UpdateCircleOneButtonText();
            // If spell is already selected, remove it
            _characterSheetService.CharacterCreationSheetInstance.RemoveSpell(currentSpell);
        }
        else
        {
            // Update the button Text
            UpdateCircleOneButtonText();
            // If spell is not selected, add it
            _characterSheetService.CharacterCreationSheetInstance.AddNewSpell(currentSpell);
        }
        SpellPointsRemaining = _characterSheetService.CharacterCreationSheetInstance.SpellPoints;
    }

    // Navigation commands for Circle 2
    [RelayCommand]
    private void PreviousCircleTwo()
    {
        if (CircleTwoSpells.Spells.Count == 0) return;
        
        CircleTwoCurrentIndex--;
        if (CircleTwoCurrentIndex < 0)
        {
            CircleTwoCurrentIndex = CircleTwoSpells.Spells.Count - 1; // Wrap to end
        }
        
        // Update button text when selection changes
        UpdateCircleTwoButtonText();
    }

    [RelayCommand]
    private void NextCircleTwo()
    {
        if (CircleTwoSpells.Spells.Count == 0) return;
        
        CircleTwoCurrentIndex++;
        if (CircleTwoCurrentIndex >= CircleTwoSpells.Spells.Count)
        {
            CircleTwoCurrentIndex = 0; // Wrap to beginning
        }
        
        // Update button text when selection changes
        UpdateCircleTwoButtonText();
    }

    [RelayCommand]
    private void SelectCircleTwo()
    {
        if (CircleTwoSpells.Spells.Count == 0) return;
        
        var currentSpell = CircleTwoSpells.Spells[CircleTwoCurrentIndex];
        
        if (IsSpellSelected(currentSpell))
        {
            //Update the button text
            UpdateCircleTwoButtonText();
            // If spell is already selected, remove it
            _characterSheetService.CharacterCreationSheetInstance.RemoveSpell(currentSpell);
        }
        else
        {
            // Update the button text
            UpdateCircleTwoButtonText();
            // If spell is not selected, add it
            _characterSheetService.CharacterCreationSheetInstance.AddNewSpell(currentSpell);
        }
        
        SpellPointsRemaining = _characterSheetService.CharacterCreationSheetInstance.SpellPoints;
    }
}