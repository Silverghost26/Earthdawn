using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using Earthdawn.Models;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public partial class DisciplinesViewModel : PageViewModel
{
    private ICharacterSheetService _characterSheetService;
    private readonly NavigationService _navigationService;
    [ObservableProperty] private int _currentIndex;

    [ObservableProperty] private Bitmap _disciplineImage;

    public ObservableCollection<DisciplineDisplayCard> Disciplines { get; }

    public DisciplinesViewModel(IDataServices dataService, ICharacterSheetService characterSheetService, NavigationService navigationService)
    {
        _characterSheetService = characterSheetService;
        PageName = ApplicationPageNames.DisciplineSelection;
        Disciplines = new ObservableCollection<DisciplineDisplayCard>(dataService.LoadDisciplines());
        foreach (DisciplineDisplayCard discipline in Disciplines)
        {
            discipline.SetPropertiesFromDictionary();
            discipline.SetDisplayForOptionalTalents();
        }
        _navigationService = navigationService;
    }

    [RelayCommand]
    private void Next() => CurrentIndex = (CurrentIndex + 1) % Disciplines.Count;

    [RelayCommand]
    private void Previous() => CurrentIndex = CurrentIndex == 0 ? Disciplines.Count - 1 : CurrentIndex - 1;

    [RelayCommand]
    private void ApplyDisciplineValues()
    {
        _characterSheetService.CharacterCreationSheetInstance.AddDiscipline(Disciplines[CurrentIndex]);
        _navigationService.NavigateTo(ApplicationPageNames.CharacterCustomizations);
    }

}