using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using Earthdawn.Interfaces;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public partial class WeaponSelectionViewModel : PageViewModel
{
    public WeaponSelectionViewModel(ICharacterSheetService characterSheetService, IDataServices dataServices, 
        ObservableCollection<EquipmentViewModel> equipmentViewModel, IEquipmentViewModel parentViewModel)
    {
        _dataServices = dataServices;
        _characterSheetService = characterSheetService;
        PageName = ApplicationPageNames.WeaponSelection; // This view model won't be directly navigated to

        // Load the weapons data
        Weapons = new ObservableCollection<WeaponDisplayCard>(_dataServices.LoadWeaponsList());
        CharacterWeapons = equipmentViewModel;
        _equipmentViewModel = parentViewModel;
    }
    
    private readonly IDataServices _dataServices;
    private readonly ICharacterSheetService _characterSheetService;
    private readonly IEquipmentViewModel _equipmentViewModel;

    // Observable collections for our weapons
    public ObservableCollection<WeaponDisplayCard> Weapons { get; }
    public ObservableCollection<EquipmentViewModel> CharacterWeapons { get; }

    // Selected index for weapon carousel
    [ObservableProperty] private int _selectedWeaponIndex = 0;

    // Property to expose the currently selected weapon
    public WeaponDisplayCard SelectedWeapon => Weapons.Count > 0 && SelectedWeaponIndex >= 0 ? Weapons[SelectedWeaponIndex] : null;
    
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
            if (_characterSheetService.CharacterCreationSheetInstance.BuyItem(SelectedWeapon.Weapons.Cost))
            {
                _characterSheetService.CharacterCreationSheetInstance.AddWeapon(SelectedWeapon.Weapons);
                CharacterWeapons.Add(new EquipmentViewModel(SelectedWeapon.Weapons));
                _equipmentViewModel.UpdateSilverRemaining(_characterSheetService.CharacterCreationSheetInstance.Money.Silver);
            }
        }
    }
}