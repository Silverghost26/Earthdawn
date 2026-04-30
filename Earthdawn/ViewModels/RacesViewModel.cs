using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Earthdawn.Data;
using Earthdawn.Models;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public partial class RacesViewModel : PageViewModel
{
    public string Test { get; set; } = "Welcome to the bound Races for Earthdawn";
    private readonly IDataServices dataService;
    private RaceDisplayCard? currentRace;
    public ObservableCollection<RaceDisplayCard> Races { get; }
    
    public RacesViewModel(IDataServices dataService)
    {
        PageName = ApplicationPageNames.Races;
        
        this.dataService = dataService;
        Races = new ObservableCollection<RaceDisplayCard>(dataService.LoadRaces());
        return;
    }
}