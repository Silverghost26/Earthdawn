using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using Earthdawn.Interfaces;
using Earthdawn.Models;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public partial class EquipmentSelectionViewModel : PageViewModel
{
    public EquipmentSelectionViewModel(ICharacterSheetService characterSheetService, IDataServices dataServices,
        ObservableCollection<EquipmentViewModel> equipmentViewModel, IEquipmentViewModel parentViewModel)
    {
        _dataServices = dataServices;
        _characterSheetService = characterSheetService;
        PageName = ApplicationPageNames.EquipmentSelection;

        // Load the equipment data
        EquipmentCards = new ObservableCollection<EquipmentDisplayCard>(_dataServices.LoadEquipmentList());
        CharacterEquipment = equipmentViewModel;
        _equipmentViewModel = parentViewModel;
    }
    
    private readonly IDataServices _dataServices;
    private readonly ICharacterSheetService _characterSheetService;
    private readonly IEquipmentViewModel _equipmentViewModel;

    // Observable collection for equipment
    public ObservableCollection<EquipmentDisplayCard> EquipmentCards { get; }
    public ObservableCollection<EquipmentViewModel> CharacterEquipment { get; }

    // Selected index for equipment carousel
    [ObservableProperty]
    private int _selectedEquipmentIndex = 0;

    // Property to expose the currently selected equipment
    public Equipment SelectedEquipment => EquipmentCards.Count > 0 && SelectedEquipmentIndex >= 0 ? EquipmentCards[SelectedEquipmentIndex].Equipment : null;

    // Equipment Navigation Commands
    [RelayCommand]
    private void PreviousEquipment()
    {
        if (EquipmentCards.Count == 0) return;

        SelectedEquipmentIndex--;
        if (SelectedEquipmentIndex < 0)
        {
            SelectedEquipmentIndex = EquipmentCards.Count - 1; // Wrap to end
        }
    }

    [RelayCommand]
    private void NextEquipment()
    {
        if (EquipmentCards.Count == 0) return;

        SelectedEquipmentIndex++;
        if (SelectedEquipmentIndex >= EquipmentCards.Count)
        {
            SelectedEquipmentIndex = 0; // Wrap to beginning
        }
    }

    [RelayCommand]
    private void SelectEquipment()
    {
        if (SelectedEquipment != null)
        {
            if (_characterSheetService.CharacterCreationSheetInstance.BuyItem(SelectedEquipment.Cost))
            {
                _characterSheetService.CharacterCreationSheetInstance.AddEquipment(SelectedEquipment);
                CharacterEquipment.Add(new EquipmentViewModel(SelectedEquipment));
                _equipmentViewModel.UpdateSilverRemaining(_characterSheetService.CharacterCreationSheetInstance.Money.Silver,
                    _characterSheetService.CharacterCreationSheetInstance.Money.Copper);
            }
        }
    }
}