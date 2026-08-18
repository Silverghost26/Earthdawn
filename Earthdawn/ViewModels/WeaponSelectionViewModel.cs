using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using Earthdawn.Models;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public partial class WeaponSelectionViewModel : PageViewModel
{
    private readonly IDataServices _dataServices;
    private readonly ICharacterSheetService _characterSheetService;

    // Observable collections for our weapons
    public ObservableCollection<WeaponDisplayCard> Weapons { get; }

    // Selected index for weapon carousel
    [ObservableProperty]
    private int _selectedWeaponIndex = 0;
    
    // Show the silver currently remaining to the character
    [ObservableProperty]
    private int _silverRemaining;

    // Property to expose the currently selected weapon
    public WeaponDisplayCard SelectedWeapon => Weapons.Count > 0 && SelectedWeaponIndex >= 0 ? Weapons[SelectedWeaponIndex] : null;

    public ObservableCollection<EquipmentViewModel> CharacterWeapons { get; }
    public ObservableCollection<EquipmentViewModel> CharacterArmor { get; }
    public ObservableCollection<EquipmentViewModel> CharacterShields { get; }
    public ObservableCollection<EquipmentViewModel> CharacterEquipment { get; }
    public ObservableCollection<EquipmentViewModel> CharacterMounts { get; }
    
    public WeaponSelectionViewModel(IDataServices dataServices, ICharacterSheetService characterSheetService)
    {
        _dataServices = dataServices;
        _characterSheetService = characterSheetService;
        PageName = ApplicationPageNames.WeaponSelection; // This view model won't be directly navigated to

        // Load the weapons data
        Weapons = new ObservableCollection<WeaponDisplayCard>(_dataServices.LoadWeaponsList());
        
        // Load silver available
        _silverRemaining =_characterSheetService.CharacterCreationSheetInstance.Money.Silver;
        
        //Assign the equipment
        CharacterWeapons =  
            new ObservableCollection<EquipmentViewModel>(_characterSheetService.CharacterCreationSheetInstance.Weapons
                .Select(w => new EquipmentViewModel(w)));
    }

    // Weapon Navigation Commands
    [RelayCommand]
    private void PreviousWeapon()
    {
        if (Weapons.Count == 0) return;

        SelectedWeaponIndex--;
        if (SelectedWeaponIndex < 0)
        {
            SelectedWeaponIndex = Weapons.Count - 1; // Wrap to end
        }
    }

    [RelayCommand]
    private void NextWeapon()
    {
        if (Weapons.Count == 0) return;

        SelectedWeaponIndex++;
        if (SelectedWeaponIndex >= Weapons.Count)
        {
            SelectedWeaponIndex = 0; // Wrap to beginning
        }
    }

    [RelayCommand]
    private void SelectWeapon()
    {
        if (SelectedWeapon != null)
        {
            _characterSheetService.CharacterCreationSheetInstance.AddWeapon(SelectedWeapon.Weapons);
            CharacterWeapons.Add(new EquipmentViewModel(SelectedWeapon.Weapons));
            _characterSheetService.CharacterCreationSheetInstance.BuyItem(SelectedWeapon.Weapons.Cost);
            SilverRemaining = _characterSheetService.CharacterCreationSheetInstance.Money.Silver;
        }
    }
}