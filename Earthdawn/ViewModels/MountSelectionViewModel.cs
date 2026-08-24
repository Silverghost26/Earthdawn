using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using Earthdawn.Interfaces;
using Earthdawn.Models;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public partial class MountSelectionViewModel : PageViewModel
{
    public MountSelectionViewModel(ICharacterSheetService characterSheetService, IDataServices dataServices,
        ObservableCollection<EquipmentViewModel> mountViewModel, IEquipmentViewModel parentViewModel)
    {
        _dataServices = dataServices;
        _characterSheetService = characterSheetService;
        PageName = ApplicationPageNames.MountSelection; // This view model won't be directly navigated to

        // Load the mounts data
        Mounts = new ObservableCollection<MountDisplayCard>(_dataServices.LoadMountsList());
        CharacterMounts = mountViewModel;
        _equipmentViewModel = parentViewModel;
    }
    
    private readonly IDataServices _dataServices;
    private readonly ICharacterSheetService _characterSheetService;
    private readonly IEquipmentViewModel _equipmentViewModel;

    // Observable collections for our mounts
    public ObservableCollection<MountDisplayCard> Mounts { get; }
    public ObservableCollection<EquipmentViewModel> CharacterMounts { get; }

    // Selected index for mount carousel
    [ObservableProperty]
    private int _selectedMountIndex = 0;

    // Property to expose the currently selected mount
    public MountDisplayCard SelectedMount => Mounts.Count > 0 && SelectedMountIndex >= 0 ? Mounts[SelectedMountIndex] : null;

    // Mount Navigation Commands
    [RelayCommand]
    private void PreviousMount()
    {
        if (Mounts.Count == 0) return;

        SelectedMountIndex--;
        if (SelectedMountIndex < 0)
        {
            SelectedMountIndex = Mounts.Count - 1; // Wrap to end
        }
    }

    [RelayCommand]
    private void NextMount()
    {
        if (Mounts.Count == 0) return;

        SelectedMountIndex++;
        if (SelectedMountIndex >= Mounts.Count)
        {
            SelectedMountIndex = 0; // Wrap to beginning
        }
    }

    [RelayCommand]
    private void SelectMount()
    {
        if (SelectedMount != null)
        {
            if (_characterSheetService.CharacterCreationSheetInstance.BuyItem(SelectedMount.Mounts.Cost))
            {
                _characterSheetService.CharacterCreationSheetInstance.AddMount(SelectedMount.Mounts);
                CharacterMounts.Add(new EquipmentViewModel(SelectedMount.Mounts));
                _equipmentViewModel.UpdateSilverRemaining(_characterSheetService.CharacterCreationSheetInstance.Money.Silver, 
                    _characterSheetService.CharacterCreationSheetInstance.Money.Copper);
            }
        }
    }
}