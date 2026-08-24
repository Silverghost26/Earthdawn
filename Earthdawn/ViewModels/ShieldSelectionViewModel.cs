using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using Earthdawn.Interfaces;
using EarthDawn.Services;
using Earthdawn.Models;

namespace Earthdawn.ViewModels;

public partial class ShieldSelectionViewModel: PageViewModel
{
    public ShieldSelectionViewModel(ICharacterSheetService characterSheetService, IDataServices dataServices,
        ObservableCollection<EquipmentViewModel> equipmentViewModel, IEquipmentViewModel parentViewModel)
    {
        _characterSheetService = characterSheetService;
        _dataServices = dataServices;
        PageName = ApplicationPageNames.ShieldSelection;
        
        Shields = new ObservableCollection<ShieldDisplayCard>(_dataServices.LoadShieldsList());
        CharacterShields = equipmentViewModel;
        _equipmentViewModel = parentViewModel;
    }
    
    private ICharacterSheetService _characterSheetService;
    private readonly IDataServices _dataServices;
    private readonly IEquipmentViewModel _equipmentViewModel;
    
    [ObservableProperty]
    private int _selectedShieldIndex = 0;
    
    public ObservableCollection<ShieldDisplayCard> Shields { get; }
    public ObservableCollection<EquipmentViewModel> CharacterShields { get; }
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
            if (_characterSheetService.CharacterCreationSheetInstance.BuyItem(SelectedShield.Shields.Cost))
            {
                _characterSheetService.CharacterCreationSheetInstance.AddShield(SelectedShield.Shields);
                CharacterShields.Add(new EquipmentViewModel(SelectedShield.Shields));
                _equipmentViewModel.UpdateSilverRemaining(_characterSheetService.CharacterCreationSheetInstance.Money.Silver, 
                    _characterSheetService.CharacterCreationSheetInstance.Money.Copper);
            }
        }
    }
}