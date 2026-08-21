using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using Earthdawn.Interfaces;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public partial class ArmorSelectionViewModel : PageViewModel
{
    public ArmorSelectionViewModel(ICharacterSheetService characterSheetService, IDataServices dataServices,
        ObservableCollection<EquipmentViewModel> equipmentViewModel, IEquipmentViewModel parentViewModel)
    {
        _characterSheetService = characterSheetService;
        _dataServices = dataServices;
        // _navigationService = navigationService;
        PageName = ApplicationPageNames.ArmorSelection;

        // Load the equipment data (excluding weapons)
        Armor = new ObservableCollection<ArmorDisplayCard>(_dataServices.LoadArmorList());
        CharacterArmor = equipmentViewModel;
        _equipmentViewModel = parentViewModel;

    }
    
    private ICharacterSheetService _characterSheetService;
    private readonly IDataServices _dataServices;
    private readonly IEquipmentViewModel _equipmentViewModel;

    // Observable collections for our equipment (excluding weapons)
    public ObservableCollection<ArmorDisplayCard> Armor { get; }
    public ObservableCollection<EquipmentViewModel> CharacterArmor { get; }
    
    // Selected indices for each carousel
    [ObservableProperty]
    private int _selectedArmorIndex = 0;
    
    // Properties to expose the currently selected items
    public ArmorDisplayCard SelectedArmor => Armor.Count > 0 && SelectedArmorIndex >= 0 ? Armor[SelectedArmorIndex] : null;

    // Armor Navigation Commands
    [RelayCommand]
    private void PreviousArmor()
    {
        if (Armor.Count == 0) return;

        SelectedArmorIndex--;
        if (SelectedArmorIndex < 0)
        {
            SelectedArmorIndex = Armor.Count - 1; // Wrap to end
        }
    }

    [RelayCommand]
    private void NextArmor()
    {
        if (Armor.Count == 0) return;

        SelectedArmorIndex++;
        if (SelectedArmorIndex >= Armor.Count)
        {
            SelectedArmorIndex = 0; // Wrap to beginning
        }
    }

    [RelayCommand]
    private void SelectArmor()
    {
        if (SelectedArmor != null)
        {
            if (_characterSheetService.CharacterCreationSheetInstance.BuyItem(SelectedArmor.Armors.Cost))
            {
                _characterSheetService.CharacterCreationSheetInstance.AddArmor(SelectedArmor.Armors);
                CharacterArmor.Add(new EquipmentViewModel(SelectedArmor.Armors));
                _equipmentViewModel.UpdateSilverRemaining(_characterSheetService.CharacterCreationSheetInstance.Money.Silver);
            }
        }
    }

}