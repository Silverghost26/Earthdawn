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
    [NotifyPropertyChangedFor(nameof(WeaponIsActive))]
    [NotifyPropertyChangedFor(nameof(ArmorIsActive))]
    [NotifyPropertyChangedFor(nameof(MountIsActive))]
    private PageViewModel _currentPage;

    public bool CharacterCustomizationsIsActive => CurrentPage.PageName == ApplicationPageNames.CharacterCustomizations;
    public bool CharacterIsActive => CurrentPage.PageName == ApplicationPageNames.CharacterCompletion;
    public bool DisciplinesIsActive => CurrentPage.PageName == ApplicationPageNames.DisciplineSelection;
    public bool EquipmentSelectionIsActive => CurrentPage.PageName == ApplicationPageNames.EquipmentSelection;
    public bool HomeIsActive => CurrentPage.PageName == ApplicationPageNames.HomePage;
    public bool RacesIsActive => CurrentPage.PageName == ApplicationPageNames.RaceSelection;
    public bool SkillsIsActive => CurrentPage.PageName == ApplicationPageNames.SkillSelection;
    public bool SpellsIsActive => CurrentPage.PageName == ApplicationPageNames.SpellSelection;
    public bool WeaponIsActive => CurrentPage.PageName == ApplicationPageNames.WeaponSelection;
    public bool ArmorIsActive => CurrentPage.PageName == ApplicationPageNames.ArmorSelection;
    public bool MountIsActive => CurrentPage.PageName == ApplicationPageNames.MountSelection;

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
    private void GoToCharacterPage() => _navigationService.GoToCharacterCompletionPage();
    
    [RelayCommand]
    private void GoToDisciplinesPage() => _navigationService.GoToDisciplineSelectionPage();
    
    [RelayCommand]
    private void GoToEquipmentSelectionPage() => _navigationService.GoToEquipmentSelectionPage();
    
    [RelayCommand]
    private void GoToRacesPage() => _navigationService.GoToRaceSelectionPage();
    
    [RelayCommand]
    private void GoToSkillsPage() => _navigationService.GoToSkillSelectionPage();
    
    [RelayCommand]
    private void GoToSpellsPage() => _navigationService.GoToSpellSelectionPage();

    [RelayCommand]
    private void WeaponSelectionPage() => _navigationService.GoToWeaponSelectionPage();
    
    [RelayCommand]
    private void ArmorSelectionPage() => _navigationService.GoToArmorSelectionPage();
    
    [RelayCommand]
    private void MountSelectionPage() => _navigationService.GoToMountSelectionPage();

    [RelayCommand]
    private void GoToNextPage() => _navigationService.GoToNextPage();

    [RelayCommand]
    private void GoToPreviousPage() => _navigationService.GoToPreviousPage();
}