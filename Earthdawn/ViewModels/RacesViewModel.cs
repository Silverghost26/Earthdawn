using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using Earthdawn.Models;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public partial class RacesViewModel : PageViewModel
{
    [ObservableProperty] 
    private int _currentIndex;

    // [ObservableProperty]
    //private Bitmap _raceImage;
    public ObservableCollection<RaceDisplayCard> Races { get; }
    

    public RacesViewModel(){}
public RacesViewModel(IDataServices dataService)
    {
        PageName = ApplicationPageNames.Races;
        Races = new ObservableCollection<RaceDisplayCard>(dataService.LoadRaces());
    }
    
    [RelayCommand]
    private void Next() => CurrentIndex = (CurrentIndex + 1) % Races.Count;   
    
    [RelayCommand]
    private void Previous() => CurrentIndex = CurrentIndex == 0 ? Races.Count - 1 : CurrentIndex - 1;
}