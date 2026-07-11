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

    // Observable collection for equipment
    public ObservableCollection<EquipmentDisplayCard> EquipmentCards { get; }

    // Selected index for equipment carousel
    [ObservableProperty]
    private int _selectedEquipmentIndex = 0;

    // Property to expose the currently selected equipment
    public Equipment SelectedEquipment => EquipmentCards.Count > 0 && SelectedEquipmentIndex >= 0 ? EquipmentCards[SelectedEquipmentIndex].Equipment : null;

    public EquipmentSelectionViewModel(IDataServices dataServices)
    {
        _dataServices = dataServices;
        PageName = ApplicationPageNames.EquipmentSelection;

        // Load the equipment data
        EquipmentCards = new ObservableCollection<EquipmentDisplayCard>(_dataServices.LoadEquipmentList());
    }

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
            // Toggle selection state
            Console.WriteLine($"(SelectedEquipment: {SelectedEquipment.Name}");
            // TODO: Implement actual selection logic here
        }
    }
}