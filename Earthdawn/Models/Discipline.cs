using System.Collections.Generic;
using System.Text.Json;
using Earthdawn.Data;

namespace Earthdawn.Models;


public class Discipline
{
    //**************************Private Fields***********************************************
    private List<Talent> _talents;
    private List<Talent> _optionalTalents;
    private List<Talent> _freeTalents;
    private List<string> _karmaSpecials;
    private List<string> _circleSpecials;
    private SpellBook _spellBook;
    
    //**************************Constructors***********************************************
    public Discipline()
    {
        _talents = new();
        _optionalTalents = new List<Talent>();
        _freeTalents = new List<Talent>();
        _karmaSpecials = new List<string>();
        _circleSpecials = new List<string>();
        _spellBook = new SpellBook();
    }

    public Discipline(Discipline discipline)
    {
        _talents = new List<Talent>();
        foreach (Talent talent in discipline._talents)
        {
            _talents.Add(new(talent));
        }
        _freeTalents = new List<Talent>();
        foreach (Talent talent in discipline._freeTalents)
        {
            _freeTalents.Add(new(talent));
        }
        _optionalTalents = new List<Talent>();
        foreach (Talent talent in discipline._optionalTalents)
        {
            _optionalTalents.Add(new(talent));
        }

        _karmaSpecials = new List<string>();
        foreach (string special in discipline._karmaSpecials)
        {
            _karmaSpecials.Add(special);
        }
        _circleSpecials = new List<string>();
        foreach (string special in discipline._circleSpecials)
        {
            _circleSpecials.Add(special);
        }

        _spellBook = new SpellBook(discipline._spellBook);
        DisciplineName = discipline.DisciplineName;
        DisciplinePrecedence = discipline.DisciplinePrecedence;
        DisciplineCircleLevel = discipline.DisciplineCircleLevel;
    }
    
    //**************************Properties***********************************
    public string DisciplineName { get; set; } = string.Empty;
    public int DisciplinePrecedence { get; set; }
    public int DisciplineCircleLevel { get; set; }
    public int Durability { get; set; }
    public int PhysicalDefenseBonus { get; set; }
    public int MysticalDefenseBonus { get; set; }
    public int SocialDefenseBonus { get; set; }
    public int PhysicalArmorBonus { get; set; }
    public int MysticalArmorBonus { get; set; }
    public int InitiativeBonus { get; set; }
    public int RecoveryTestBonus { get; set; }
    
    //**************************Functions***********************************
    public int RemoveOptionalTalent(string talent)
    {
        foreach (Talent t in _optionalTalents)
        {
            if (t.Name == talent)
            {
                _optionalTalents.Remove(t);
                return t.Rank;
            }
        }
        return 0;
    }

    public int RemoveTalent(string talent)
    {
        foreach (Talent t in _talents)
        {
            if (t.Name == talent)
            {
                _talents.Remove(t);
                return t.Rank;
            }
        }

        return 0;
    }
    
    public ref readonly SpellBook GetSpellBook()
    {
        return ref _spellBook;
    }
    public ref readonly List<Talent> GetDisciplineTalents()
    {
        return ref _talents;
    }
    public ref readonly List<Talent> GetDisciplineOptionalTalents()
    {
        return ref _optionalTalents;
    }
    public ref readonly List<Talent> GetDisciplineFreeTalents()
    {
        return ref _freeTalents;
    }

    public ref readonly List<string> GetDisciplineCircleSpecials()
    {
        return ref _circleSpecials;
    }

    public ref readonly List<string> GetDisciplineKarmaSpecials()
    {
        return ref _karmaSpecials;   
    }

    public void AdvanceDisciplineCircle(DisciplineCircle dc)
    {
        //TODO: Advance Discipline Circle
    }
    
    public void AddNewOptionalTalent(Talent talent)
    {
        if (_optionalTalents.Contains(talent))
            return;
        _optionalTalents.Add(new(talent));
    }
    
    public void AddNewTalent(Talent talent)
    {
        if (_talents.Contains(talent))
            return;
        _talents.Add(new(talent));
    }

    public void AddNewFreeTalent(Talent talent)
    {
        if (_freeTalents.Contains(talent))
            return;
        Talent newFreeTalent = new(talent);
        newFreeTalent.Rank = DisciplineCircleLevel;
        _freeTalents.Add(newFreeTalent);   
    }

    public bool AddNewSpell(Spell spell)
    {
        return _spellBook.AddSpell(new Spell(spell));
    }

    public bool RemoveSpell(Spell spell)
    {
        return _spellBook.RemoveSpell(spell);
    }
    
    public void AddNewKarmaSpecial(string special)
    {
        _karmaSpecials.Add(special);
    }
    
    public void AddNewCircleSpecial(string special)
    {
        _circleSpecials.Add(special);
    }
}

