using System.Collections.ObjectModel;
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
    [ObservableProperty] private int _currentIndex;

    [ObservableProperty] private Bitmap _disciplineImage;

    public ObservableCollection<DisciplineDisplayCard> Disciplines { get; }

    public DisciplinesViewModel(IDataServices dataService, ICharacterSheetService characterSheetService)
    {
        _characterSheetService = characterSheetService;
        PageName = ApplicationPageNames.Disciplines;
        Disciplines = new ObservableCollection<DisciplineDisplayCard>(dataService.LoadDisciplines());
        foreach (DisciplineDisplayCard discipline in Disciplines)
        {
            discipline.SetPropertiesFromDictionary();
            discipline.SetDisplayForOptionalTalents();
        }

    }

    [RelayCommand]
    private void Next() => CurrentIndex = (CurrentIndex + 1) % Disciplines.Count;

    [RelayCommand]
    private void Previous() => CurrentIndex = CurrentIndex == 0 ? Disciplines.Count - 1 : CurrentIndex - 1;

    [RelayCommand]
    private void ApplyDisciplineValues()
    {
        Discipline discipline = Disciplines[CurrentIndex].Disciplines;
        _characterSheetService.CharacterSheetInstance.CharacterDiscipline = discipline;

        _characterSheetService.CharacterSheetInstance.Discipline = Disciplines[CurrentIndex].Name;

    }

}