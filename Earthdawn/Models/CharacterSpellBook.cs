using System.Collections.Generic;

namespace Earthdawn.Models;

public class CharacterSpellBook
{
    // private Dictionary<int, List<CharacterSpell>> _spellBook;
    //
    // public CharacterSpellBook()
    // {
    //  _spellBook = new Dictionary<int, List<CharacterSpell>>();   
    // }
    //
    // public List<CharacterSpell> GetSpells(int spellCircle)
    // {
    //     if (_spellBook.Count <= 0)
    //         return new List<CharacterSpell>();
    //     if (_spellBook.ContainsKey(spellCircle))
    //     {
    //         List<CharacterSpell> newSpellList = new List<CharacterSpell>();
    //         foreach(CharacterSpell spell in _spellBook[spellCircle])
    //         {
    //             newSpellList.Add(new CharacterSpell(spell));
    //         }
    //
    //         return newSpellList;
    //     }
    //     return new List<CharacterSpell>();
    // }
    //
    // public void AddNewSpell(CharacterSpell newSpell, int spellCircle)
    // {
    //     if (!_spellBook.ContainsKey(spellCircle))
    //         return;
    //     if (_spellBook[spellCircle].Contains(newSpell))
    //         return;
    //     _spellBook[spellCircle].Add(new CharacterSpell(newSpell));
    // }
    //
    // public void AddNewSpell(Spell spell, int spellCircle)
    // {
    //     CharacterSpell cs = new CharacterSpell(spell);
    //     AddNewSpell(cs, spellCircle);
    // }
    //
    // public void RemoveSpell(CharacterSpell spell)
    // {
    //     foreach (KeyValuePair<int, List<CharacterSpell>> spellCircle in _spellBook)
    //     {
    //         spellCircle.Value.Remove(spell);
    //     }
    // }
    
    
}