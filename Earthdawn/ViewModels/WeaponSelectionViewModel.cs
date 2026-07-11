using System;
using System.Collections.ObjectModel;
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

    // Property to expose the currently selected weapon
    public WeaponDisplayCard SelectedWeapon => Weapons.Count > 0 && SelectedWeaponIndex >= 0 ? Weapons[SelectedWeaponIndex] : null;

    public WeaponSelectionViewModel(IDataServices dataServices, ICharacterSheetService characterSheetService)
    {
        _dataServices = dataServices;
        _characterSheetService = characterSheetService;
        PageName = ApplicationPageNames.WeaponSelection; // This view model won't be directly navigated to

        // Load the weapons data
        Weapons = new ObservableCollection<WeaponDisplayCard>(_dataServices.LoadWeaponsList());
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
            Console.WriteLine($"Selected weapon: {SelectedWeapon.Name}");
            // TODO: Implement actual selection logic here
            // For example, you might want to update the character sheet:
            // _characterSheetService.UpdateWeapon(SelectedWeapon);
        }
    }
}