using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using EarthDawn.Services;
using Earthdawn.Models;

namespace Earthdawn.ViewModels;

public partial class ShieldSelectionViewModel: PageViewModel
{
    public ShieldSelectionViewModel(ICharacterSheetService characterSheetService, IDataServices dataServices,
        NavigationService navigationService)
    {
        _characterSheetService = characterSheetService;
        _dataServices = dataServices;
        _navigationService = navigationService;
        PageName = ApplicationPageNames.ShieldSelection;
        Shields = new ObservableCollection<ShieldDisplayCard>(_dataServices.LoadShieldsList());
    }
    
    private ICharacterSheetService _characterSheetService;
    private readonly IDataServices _dataServices;
    private readonly NavigationService _navigationService;
    
    [ObservableProperty]
    private int _selectedShieldIndex = 0;
    
    public ObservableCollection<ShieldDisplayCard> Shields { get; }
    public ShieldDisplayCard SelectedShield => Shields.Count > 0 && SelectedShieldIndex >= 0 ? Shields[SelectedShieldIndex] : null;
    
    
    // Shield Navigation Commands
    [RelayCommand]
    private void PreviousShield()
    {
        if (Shields.Count == 0) return;

        SelectedShieldIndex--;
        if (SelectedShieldIndex < 0)
        {
            SelectedShieldIndex = Shields.Count - 1; // Wrap to end
        }
    }

    [RelayCommand]
    private void NextShield()
    {
        if (Shields.Count == 0) return;

        SelectedShieldIndex++;
        if (SelectedShieldIndex >= Shields.Count)
        {
            SelectedShieldIndex = 0; // Wrap to beginning
        }
    }

    [RelayCommand]
    private void SelectShield()
    {
        if (SelectedShield != null)
        {
            Console.WriteLine($"Selected shield: {SelectedShield.Name}");
            // TODO: Implement actual selection logic here
        }
    }
}