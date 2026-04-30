using Earthdawn.Data;

namespace Earthdawn.ViewModels;

public partial class EquipmentSelectionViewModel : PageViewModel
{
    public string Test { get; set; } = "Welcome to the bound Equipment for Earthdawn";
    public EquipmentSelectionViewModel()
    {
        PageName = ApplicationPageNames.EquipmentSelection;
    }
   
}