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
    [ObservableProperty]
    private int _currentIndex;
    
    [ObservableProperty]
    private Bitmap _disciplineImage;
    
    public ObservableCollection<DisciplineDisplayCard> Disciplines { get; }
    
    public DisciplinesViewModel(IDataServices dataService)
    {
        PageName = ApplicationPageNames.Disciplines;
        Disciplines = new ObservableCollection<DisciplineDisplayCard>(dataService.LoadDisciplines());
        foreach (DisciplineDisplayCard discipline in Disciplines)
        {
            discipline.SetPropertiesFromDictionary();
        }
        
    }
    
    [RelayCommand]
    private void Next() => CurrentIndex = (CurrentIndex + 1) % Disciplines.Count;

    [RelayCommand]
    private void Previous() => CurrentIndex = CurrentIndex == 0 ? Disciplines.Count - 1 : CurrentIndex - 1;
}