using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using Earthdawn.Models;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public partial class RacesViewModel : PageViewModel
{
    public string Test { get; set; } = "Welcome to the bound Races for Earthdawn";

    //private RaceDisplayCard? currentRace;
    [ObservableProperty] 
    private int _currentIndex;
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