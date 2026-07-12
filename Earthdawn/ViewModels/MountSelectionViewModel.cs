using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using Earthdawn.Models;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public partial class MountSelectionViewModel : PageViewModel
{
    private readonly IDataServices _dataServices;
    private readonly ICharacterSheetService _characterSheetService;

    // Observable collections for our mounts
    public ObservableCollection<MountDisplayCard> Mounts { get; }

    // Selected index for mount carousel
    [ObservableProperty]
    private int _selectedMountIndex = 0;

    // Property to expose the currently selected mount
    public MountDisplayCard SelectedMount => Mounts.Count > 0 && SelectedMountIndex >= 0 ? Mounts[SelectedMountIndex] : null;

    public MountSelectionViewModel(IDataServices dataServices, ICharacterSheetService characterSheetService)
    {
        _dataServices = dataServices;
        _characterSheetService = characterSheetService;
        PageName = ApplicationPageNames.MountSelection; // This view model won't be directly navigated to

        // Load the mounts data
        Mounts = new ObservableCollection<MountDisplayCard>(_dataServices.LoadMountsList());
    }

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
            Console.WriteLine($"Selected mount: {SelectedMount.Name}");
            // TODO: Implement actual selection logic here
            // For example, you might want to update the character sheet:
            // _characterSheetService.UpdateMount(SelectedMount);
        }
    }
}