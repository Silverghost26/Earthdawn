// Earthdawn/ViewModels/SpellCollectionViewModel.cs

using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Earthdawn.Models;

namespace Earthdawn.ViewModels;

public partial class SpellCollectionViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Spell> _spells = new();

    public SpellCollectionViewModel() { }

    public SpellCollectionViewModel(IEnumerable<Spell> spells)
    {
        Spells = new ObservableCollection<Spell>(spells);
    }
}
