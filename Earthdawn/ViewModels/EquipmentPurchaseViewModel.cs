using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EarthDawn.Services;
using Earthdawn.Interfaces;
using Earthdawn.Models;
using EarthDawn.Models;

namespace Earthdawn.ViewModels;

public partial class EquipmentPurchaseViewModel : PageViewModel, IEquipmentViewModel
{
    public EquipmentPurchaseViewModel(ICharacterSheetService characterSheetService, IDataServices dataServices,
        NavigationService navigationService)
    {
        _navigationSerivces = navigationService;
        _characterSheetService = characterSheetService;
        _dataServices = dataServices;
        
        // Load coin available
        _silverRemaining =_characterSheetService.CharacterCreationSheetInstance.Money.Silver;
        _copperRemaining =_characterSheetService.CharacterCreationSheetInstance.Money.Copper;
        
        //Assign the equipment
        CharacterWeapons = 
            new ObservableCollection<EquipmentViewModel>(_characterSheetService.CharacterCreationSheetInstance.Weapons
                .Select(w => new EquipmentViewModel(w)));
        CharacterArmor = 
            new ObservableCollection<EquipmentViewModel>(_characterSheetService.CharacterCreationSheetInstance.Weapons
                .Select(w => new EquipmentViewModel(w)));
        CharacterShields = 
            new ObservableCollection<EquipmentViewModel>(_characterSheetService.CharacterCreationSheetInstance.Weapons
                .Select(w => new EquipmentViewModel(w)));
        CharacterEquipment = 
            new ObservableCollection<EquipmentViewModel>(_characterSheetService.CharacterCreationSheetInstance.Weapons
                .Select(w => new EquipmentViewModel(w)));
        CharacterMounts = 
            new ObservableCollection<EquipmentViewModel>(_characterSheetService.CharacterCreationSheetInstance.Weapons
                .Select(w => new EquipmentViewModel(w)));
        // CharacterCloths = 
        //     new ObservableCollection<EquipmentViewModel>(_characterSheetService.CharacterCreationSheetInstance.Weapons
        //         .Select(w => new EquipmentViewModel(w)));
        
        this.NavigateToWeapons();
    } 
    
    private readonly NavigationService _navigationSerivces;
    private readonly IDataServices _dataServices;
    private ICharacterSheetService _characterSheetService;
    
    [ObservableProperty] private PageViewModel _currentChildViewModel;
    // Show the coin currently remaining to the character
    [ObservableProperty] private int _silverRemaining;
    [ObservableProperty] private int _copperRemaining;
    [ObservableProperty] private EquipmentViewModel? _selectedEquipment;
    
    public ObservableCollection<EquipmentViewModel> CharacterWeapons { get; }
    public ObservableCollection<EquipmentViewModel> CharacterArmor { get; }
    public ObservableCollection<EquipmentViewModel> CharacterShields { get; }
    public ObservableCollection<EquipmentViewModel> CharacterEquipment { get; }
    public ObservableCollection<EquipmentViewModel> CharacterMounts { get; }

    partial void OnSelectedEquipmentChanged(EquipmentViewModel? value)
    {
        if (value == null)
            return;
        if (value.Equipment is Weapon)
        {
            Avalonia.Threading.Dispatcher.UIThread.Post(() =>
                {
                    CharacterWeapons.Remove(value);
                }
            );
        }
        else if (value.Equipment is Armor)
        {
            Avalonia.Threading.Dispatcher.UIThread.Post(() =>
                {
                    CharacterArmor.Remove(value);
                }
            );
        }
        else if (value.Equipment is Shield)
        {
            Avalonia.Threading.Dispatcher.UIThread.Post(() =>
                {
                    CharacterShields.Remove(value);
                }
            );
        }
        else if (value.Equipment is Equipment)
        {
            Avalonia.Threading.Dispatcher.UIThread.Post(() =>
                {
                    CharacterEquipment.Remove(value);
                }
            );
        }
        else if (value.Equipment is Mount)
        {
            Avalonia.Threading.Dispatcher.UIThread.Post(() =>
                {
                    CharacterMounts.Remove(value);
                }
            );
        }
        // else if (value.Equipment is Colths)
        // {
        //     Avalonia.Threading.Dispatcher.UIThread.Post(() =>
        //         {
        //             CharacterCloths.Remove(value);
        //         }
        //     );
        // }
        ReturnItem(value.Cost);
    }

    public void ReturnItem(float cost)
    {
        _characterSheetService.CharacterCreationSheetInstance.ReturnItem(cost);
        UpdateSilverRemaining(_characterSheetService.CharacterCreationSheetInstance.Money.Silver, 
            _characterSheetService.CharacterCreationSheetInstance.Money.Copper);
    }
    public void UpdateSilverRemaining(int silverRemaining, int copperRemaining)
    {
        SilverRemaining = silverRemaining;
        CopperRemaining = copperRemaining;
    }
    
    [RelayCommand]
    private void NavigateToWeapons()
    {
        CurrentChildViewModel = new WeaponSelectionViewModel(_characterSheetService, _dataServices, CharacterWeapons, this);
    }
    [RelayCommand]
    private void NavigateToArmor()
    {
        CurrentChildViewModel = new ArmorSelectionViewModel(_characterSheetService, _dataServices, CharacterArmor, this);
    }
    
    [RelayCommand]
    private void NavigateToShields()
    {
        CurrentChildViewModel = new ShieldSelectionViewModel(_characterSheetService, _dataServices, CharacterShields, this);
    }
    [RelayCommand]
    private void NavigateToEquipment()
    {
        CurrentChildViewModel = new EquipmentSelectionViewModel(_characterSheetService, _dataServices, CharacterEquipment, this);
    }
    
    [RelayCommand]
    private void NavigateToMounts()
    {
        CurrentChildViewModel = new MountSelectionViewModel(_characterSheetService, _dataServices, CharacterMounts, this);
    }
    
    [RelayCommand]
    private void NavigateToCloths()
    {
        //CurrentChildViewModel = new ClothsSelectionViewModel(_dataServices, _characterSheetService);
    }

    [RelayCommand]
    private void SaveAndContinueToCharacterSummary()
    {
        _navigationSerivces.GoToCharacterCompletionPage();
    }
}


