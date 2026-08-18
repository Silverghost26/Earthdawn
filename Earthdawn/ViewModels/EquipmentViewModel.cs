using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Models;

namespace Earthdawn.ViewModels;

public partial class EquipmentViewModel : ObservableObject
{
    private readonly EquipmentBase _equipment;

    [ObservableProperty] private string _name;
    [ObservableProperty] private string _description;
    [ObservableProperty] private string _availability;
    [ObservableProperty] private string _weight;
    [ObservableProperty] private float _cost;

    private readonly EquipmentBase? _equipmentBase;
    
    public EquipmentViewModel(EquipmentBase equipment)
    {
        _equipment = equipment;
        _name = equipment.Name;
        _description = equipment.Description;
        _availability = equipment.Availability;
        _weight = equipment.Weight;
        _cost = equipment.Cost;
    }
}