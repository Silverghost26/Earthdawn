using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EarthDawn.Services;
using Earthdawn.Interfaces;


namespace Earthdawn.ViewModels;

public partial class EquipmentPurchaseViewModel : PageViewModel, IEquipmentViewModel
{
    public EquipmentPurchaseViewModel(ICharacterSheetService characterSheetService, IDataServices dataServices,
        NavigationService navigationService)
    {
        _navigationSerivces = navigationService;
        _characterSheetService = characterSheetService;
        _dataServices = dataServices;
        
        // Load silver available
        _silverRemaining =_characterSheetService.CharacterCreationSheetInstance.Money.Silver;
        
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
    // Show the silver currently remaining to the character
    [ObservableProperty] private int _silverRemaining;
    
    public ObservableCollection<EquipmentViewModel> CharacterWeapons { get; }
    public ObservableCollection<EquipmentViewModel> CharacterArmor { get; }
    public ObservableCollection<EquipmentViewModel> CharacterShields { get; }
    public ObservableCollection<EquipmentViewModel> CharacterEquipment { get; }
    public ObservableCollection<EquipmentViewModel> CharacterMounts { get; }
    
    public void UpdateSilverRemaining(int silverRemaining)
    {
        SilverRemaining = silverRemaining;
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
}


