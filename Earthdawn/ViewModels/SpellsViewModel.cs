using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;
using Avalonia.Controls.Documents;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using Earthdawn.Models;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public partial class SpellsViewModel : PageViewModel
{
    [ObservableProperty] private int _currentIndex;
    public ObservableCollection<SpellDisplayCard> Spells { get; }
    private ICharacterSheetService _characterSheetService;
    
    public SpellsViewModel(IDataServices dataServices, ICharacterSheetService characterSheetService)
    {
        _characterSheetService =  characterSheetService;
        PageName = ApplicationPageNames.Spells;
        Spells = new ObservableCollection<SpellDisplayCard>(dataServices.LoadSpells());
        int i = 0;
    }
}