using System.Collections.Generic;
using System.Linq;

namespace Earthdawn.Models;

public class SpellBook
{
    private List<Spell> _spells;

    public SpellBook()
    {
        _spells = new();
    }

    public SpellBook(SpellBook spellBook)
    {
        _spells = new();
        foreach(var spell in spellBook._spells)
        {
            _spells.Add(new Spell(spell));
        }
    }
    //TODO: Add Spell Matrices : Stored threads, spells current in matrix, etc.
    
    public List<Spell> Spells => _spells;
    public bool AddSpell(Spell spell)
    {
        if (_spells.Any(item => item.Name == spell.Name))
            return false;
        _spells.Add(new Spell(spell));
        return true;
    }

    public bool RemoveSpell(Spell spell)
    {
        if (!_spells.Any(item => item.Name == spell.Name))
            return false;
        _spells.RemoveAll(item => item.Name == spell.Name);
        return true;
    }
}