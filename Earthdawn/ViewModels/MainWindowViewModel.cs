using CommunityToolkit.Mvvm.ComponentModel;
using Earthdawn.Data;
using Earthdawn.Factories;
using System;
using System.Reflection.Metadata.Ecma335;
using CommunityToolkit.Mvvm.Input;

namespace Earthdawn.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private PageFactory _pageFactory;
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
    [NotifyPropertyChangedFor(nameof(TalentsIsActive))]
    private PageViewModel _currentPage;

    public bool CharacterCustomizationsIsActive => CurrentPage.PageName == ApplicationPageNames.CharacterCustomizations;
    public bool CharacterIsActive => CurrentPage.PageName == ApplicationPageNames.Character;
    public bool DisciplinesIsActive => CurrentPage.PageName == ApplicationPageNames.Disciplines;
    public bool EquipmentSelectionIsActive => CurrentPage.PageName == ApplicationPageNames.EquipmentSelection;
    public bool HomeIsActive => CurrentPage.PageName == ApplicationPageNames.Home;
    public bool RacesIsActive => CurrentPage.PageName == ApplicationPageNames.Races;
    public bool SkillsIsActive => CurrentPage.PageName == ApplicationPageNames.Skills;
    public bool SpellsIsActive => CurrentPage.PageName == ApplicationPageNames.Spells;
    public bool TalentsIsActive => CurrentPage.PageName == ApplicationPageNames.Talents;

    public MainWindowViewModel()
    {
        
    }
    
    public MainWindowViewModel(PageFactory pageFactory)
    {
        PreviousPageIsAvailable = false;
        NextPageIsAvailable = true;
        _pageFactory = pageFactory;
        
        GoToHomePage();
    }
    
    [RelayCommand]
    private void GoToHomePage() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Home);
    [RelayCommand]
    private void GoToCharacterCustomizationPage() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.CharacterCustomizations);
    [RelayCommand]
    private void GoToCharacterPage() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Character);
    [RelayCommand]
    private void GoToDisciplinesPage() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Disciplines);
    [RelayCommand]
    private void GoToEquipmentSelectionPage() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.EquipmentSelection);
    [RelayCommand]
    private void GoToRacesPage() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Races);
    [RelayCommand]
    private void GoToSkillsPage() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Skills);
    [RelayCommand]
    private void GoToSpellsPage() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Spells);
    [RelayCommand]
    private void GoToTalentsPage() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Talents);

    [RelayCommand]
    private void GoToNextPage()
    {
        switch (CurrentPage.PageName)
        {
            case ApplicationPageNames.Home:
                GoToRacesPage();
                break;
            case ApplicationPageNames.Races:
                GoToDisciplinesPage();
                break;
            case ApplicationPageNames.Disciplines:
                GoToCharacterCustomizationPage();
                break;
            case ApplicationPageNames.CharacterCustomizations:
                GoToTalentsPage();
                break;
            case ApplicationPageNames.Talents:
                GoToSkillsPage();
                break;
            case ApplicationPageNames.Skills:
                GoToSpellsPage();
                break;
            case ApplicationPageNames.Spells:
                GoToEquipmentSelectionPage();
                break;
            case ApplicationPageNames.EquipmentSelection:
                GoToCharacterPage();
                break;
            case ApplicationPageNames.Character:
                break;
            default:
                break;
        }
        PreviousPageIsAvailable = !HomeIsActive;
        NextPageIsAvailable = !CharacterIsActive;
    }

    [RelayCommand]
    private void GoToPreviousPage()
    {
        switch (CurrentPage.PageName)
        {
            case ApplicationPageNames.Home:
                break;
            case ApplicationPageNames.Races:
                GoToHomePage();
                break;
            case ApplicationPageNames.Disciplines:
                GoToRacesPage();
                break;
            case ApplicationPageNames.CharacterCustomizations:
                GoToDisciplinesPage();
                break;
            case ApplicationPageNames.Talents:
                GoToCharacterCustomizationPage();
                break;
            case ApplicationPageNames.Skills:
                GoToTalentsPage();
                break;
            case ApplicationPageNames.Spells:
                GoToSkillsPage();
                break;
            case ApplicationPageNames.EquipmentSelection:
                GoToSpellsPage();
                break;
            case ApplicationPageNames.Character:
                GoToEquipmentSelectionPage();
                break;
            default:
                break;
        }
        PreviousPageIsAvailable = !HomeIsActive;
        NextPageIsAvailable = !CharacterIsActive;
    }
}