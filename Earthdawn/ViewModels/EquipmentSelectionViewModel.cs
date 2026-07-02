using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using Earthdawn.Models;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public partial class EquipmentSelectionViewModel : PageViewModel
{
    private readonly IDataServices _dataServices;

    public string Test { get; set; } = "Welcome to the bound Equipment for Earthdawn";

    // Observable collections for our equipment
    public ObservableCollection<WeaponDisplayCard> Weapons { get; }
    public ObservableCollection<ArmorDisplayCard> Armor { get; }
    public ObservableCollection<ShieldDisplayCard> Shields { get; }

    // Selected indices for each carousel
    [ObservableProperty]
    private int _selectedWeaponIndex = 0;

    [ObservableProperty]
    private int _selectedArmorIndex = 0;

    [ObservableProperty]
    private int _selectedShieldIndex = 0;

    // Properties to expose the currently selected items
    public WeaponDisplayCard SelectedWeapon => Weapons.Count > 0 && SelectedWeaponIndex >= 0 ? Weapons[SelectedWeaponIndex] : null;
    public ArmorDisplayCard SelectedArmor => Armor.Count > 0 && SelectedArmorIndex >= 0 ? Armor[SelectedArmorIndex] : null;
    public ShieldDisplayCard SelectedShield => Shields.Count > 0 && SelectedShieldIndex >= 0 ? Shields[SelectedShieldIndex] : null;

    public EquipmentSelectionViewModel(IDataServices dataServices)
    {
        _dataServices = dataServices;
        PageName = ApplicationPageNames.EquipmentSelection;

        // Load the equipment data
        Weapons = new ObservableCollection<WeaponDisplayCard>(_dataServices.LoadWeaponsList());
        Armor = new ObservableCollection<ArmorDisplayCard>(_dataServices.LoadArmorList());
        Shields = new ObservableCollection<ShieldDisplayCard>(_dataServices.LoadShieldsList());
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
        }
    }

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
            Console.WriteLine($"Selected armor: {SelectedArmor.Name}");
            // TODO: Implement actual selection logic here
        }
    }

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