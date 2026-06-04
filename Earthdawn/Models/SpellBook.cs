using System.Collections.Generic;

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
    public void AddSpell(Spell spell)
    {
        _spells.Add(new Spell(spell));
    }
}