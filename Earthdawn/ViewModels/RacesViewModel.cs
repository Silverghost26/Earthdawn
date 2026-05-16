using System;
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
    [ObservableProperty] private int _currentIndex;
    public ObservableCollection<RaceDisplayCard> Races { get; }
    private ICharacterSheetService _characterSheetService;
    

    public RacesViewModel(){}
    
    public RacesViewModel(IDataServices dataService, ICharacterSheetService characterSheetService)
    {
        _characterSheetService = characterSheetService;
        PageName = ApplicationPageNames.Races;
        Races = new ObservableCollection<RaceDisplayCard>(dataService.LoadRaces());
        foreach(RaceDisplayCard race in Races)
        {
            race.CalculateAbilitiesStringBreakPoint();
        }
    }
    
    [RelayCommand]
    private void Next()
    {
        CurrentIndex = (CurrentIndex + 1) % Races.Count; 
    }

    [RelayCommand]
    private void Previous()
    {
        CurrentIndex = CurrentIndex == 0 ? Races.Count - 1 : CurrentIndex - 1;
    } 
    
    [RelayCommand]
    private void ApplyRaceAttributes()
    {
        Race race= Races[CurrentIndex].NameGiverRace;
        _characterSheetService.CharacterCreationSheetInstance.AddRaceBaseAttributes(race);
        _characterSheetService.CharacterCreationSheetInstance.MovementRate = race.Movement;
        _characterSheetService.CharacterCreationSheetInstance.FlyingMovementRate = race.FlyingMovement;
        _characterSheetService.CharacterCreationSheetInstance.KarmaModifier = race.KarmaMod;
        _characterSheetService.CharacterCreationSheetInstance.RacialAbilities = race.Abilities;
        _characterSheetService.CharacterCreationSheetInstance.Race = Races[CurrentIndex].Name;
    }
}