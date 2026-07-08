using CommunityToolkit.Mvvm.ComponentModel;
using Earthdawn.Data;
using System;
using CommunityToolkit.Mvvm.Input;
using EarthDawn.Services;


namespace Earthdawn.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly NavigationService _navigationService;
    
    [ObservableProperty] private bool _previousPageIsAvailable;
    [ObservableProperty] private bool _nextPageIsAvailable;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CharacterCustomizationsIsActive))]
    [NotifyPropertyChangedFor(nameof(CharacterIsActive))]
    [NotifyPropertyChangedFor(nameof(DisciplinesIsActive))]
    [NotifyPropertyChangedFor(nameof(EquipmentSelectionIsActive))]
    [NotifyPropertyChangedFor(nameof(HomeIsActive))]
    [NotifyPropertyChangedFor(nameof(RacesIsActive))]
    [NotifyPropertyChangedFor(nameof(SkillsIsActive))]
    [NotifyPropertyChangedFor(nameof(SpellsIsActive))]
    private PageViewModel _currentPage;

    public bool CharacterCustomizationsIsActive => CurrentPage.PageName == ApplicationPageNames.CharacterCustomizations;
    public bool CharacterIsActive => CurrentPage.PageName == ApplicationPageNames.Character;
    public bool DisciplinesIsActive => CurrentPage.PageName == ApplicationPageNames.Disciplines;
    public bool EquipmentSelectionIsActive => CurrentPage.PageName == ApplicationPageNames.EquipmentSelection;
    public bool HomeIsActive => CurrentPage.PageName == ApplicationPageNames.Home;
    public bool RacesIsActive => CurrentPage.PageName == ApplicationPageNames.Races;
    public bool SkillsIsActive => CurrentPage.PageName == ApplicationPageNames.Skills;
    public bool SpellsIsActive => CurrentPage.PageName == ApplicationPageNames.Spells;

    public MainWindowViewModel()
    {
        
    }
    
    public MainWindowViewModel(NavigationService navigationService)
    {
        _navigationService = navigationService;
        _navigationService.CurrentPageChanged += OnCurrentPageChanged;
        
        PreviousPageIsAvailable = false;
        NextPageIsAvailable = true;
        
        GoToHomePage();
    }
    
    private void OnCurrentPageChanged(object? sender, EventArgs e)
    {
        CurrentPage = _navigationService.CurrentPage;
        PreviousPageIsAvailable = !HomeIsActive;
        NextPageIsAvailable = !CharacterIsActive;
    }
    
    [RelayCommand]
    private void GoToHomePage() => _navigationService.GoToHomePage();
    
    [RelayCommand]
    private void GoToCharacterCustomizationPage() => _navigationService.GoToCharacterCustomizationPage();
    
    [RelayCommand]
    private void GoToCharacterPage() => _navigationService.GoToCharacterPage();
    
    [RelayCommand]
    private void GoToDisciplinesPage() => _navigationService.GoToDisciplinesPage();
    
    [RelayCommand]
    private void GoToEquipmentSelectionPage() => _navigationService.GoToEquipmentSelectionPage();
    
    [RelayCommand]
    private void GoToRacesPage() => _navigationService.GoToRacesPage();
    
    [RelayCommand]
    private void GoToSkillsPage() => _navigationService.GoToSkillsPage();
    
    [RelayCommand]
    private void GoToSpellsPage() => _navigationService.GoToSpellsPage();

    [RelayCommand]
    private void GoToNextPage() => _navigationService.GoToNextPage();

    [RelayCommand]
    private void GoToPreviousPage() => _navigationService.GoToPreviousPage();
}