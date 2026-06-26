using System.Collections.ObjectModel;
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

    public ObservableCollection<SpellDisplayCard> Spells { get; }
    private ICharacterSheetService _characterSheetService;
    private IDataServices _dataServices;

    public SpellsViewModel(IDataServices dataServices, ICharacterSheetService characterSheetService)
    {
        _characterSheetService = characterSheetService;
        _dataServices = dataServices;
        PageName = ApplicationPageNames.Spells;
        Spells = new ObservableCollection<SpellDisplayCard>(dataServices.LoadSpells());
        DisciplineName = _characterSheetService.CharacterCreationSheetInstance.GetDisciplines()[0].DisciplineName;
        
        // Initialize with first discipline
        UpdateSpellsForDiscipline();
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

    // Navigation commands for Circle 1
    [RelayCommand]
    private void PreviousCircleOne()
    {
        if (CircleOneSpells.Spells.Count == 0) return;
        
        CircleOneCurrentIndex--;
        if (CircleOneCurrentIndex < 0)
        {
            CircleOneCurrentIndex = CircleOneSpells.Spells.Count - 1; // Wrap to end
        }
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
    }
}