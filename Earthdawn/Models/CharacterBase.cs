using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using CommunityToolkit.Mvvm.ComponentModel;
using Earthdawn.Data;
using EarthDawn.Services;

namespace Earthdawn.Models;
public class CharacterBase
{
    //**********************************************Private Members********************************************
    protected List<Discipline> _disciplines;
    //**********************************************Constructors***********************************
    public CharacterBase()
    {
        _charAttributes = new Attributes();
        _disciplines = new ();
    }
    
    //***********************************Private Vars*************************************************
    protected Attributes? _charAttributes;
    protected void SetCharAttributes(Attributes? value)
    {
        _charAttributes = value;
    }
        
    //*****************************************Properties**********************************************
    public string CharacterName { get; set; }
    public string Race { get; set; }
    public List<SpecialAbility> RacialAbilities
    {
        get => _racialAbilities ??= new List<SpecialAbility>();
        set => _racialAbilities = value ?? new List<SpecialAbility>();
    }
    private List<SpecialAbility> _racialAbilities;
    public string Sex { get; set; }
    public int Age { get; set; }
    public string Height { get; set; }
    public string Weight { get; set; }
    public string HairColor { get; set; }
    public string EyeColor { get; set; }
    public string SkinColor { get; set; }
    public int MovementRate { get; set; }
    public int FlyingMovementRate { get; set; }
    public int CarryingCapacity { get; set; }

 public int Dexterity
    {
        get
        {
            return _charAttributes.Dexterity;
        }
        set
        {
            _charAttributes.Dexterity = value;
        }
    }
    public int Strength
    {
        get
        {
            return _charAttributes.Strength;
        }
        set
        {
            _charAttributes.Strength = value;
        }
    }
    public int Toughness {
        get
        {
            return _charAttributes.Toughness;
        }
        set
        {
            _charAttributes.Toughness = value;
        }
    }

    public int Perception
    {
        get
        {
            return _charAttributes.Perception;
        }
        set
        {
            _charAttributes.Perception = value;
        }
    }

    public int Willpower
    {
        get
        {
            return _charAttributes.Willpower;
        }
        set
        {
            _charAttributes.Willpower = value;
        }
    }

    public int Charisma
    {
        get
        {
            return _charAttributes.Charisma;
        }
        set
        {
            _charAttributes.Charisma = value;
        }
    }
    
    public int Initiative {
        get
        {
            return _charAttributes.GetStepNumber(AttributesTypes.Dex);
        }
        set; 
    }

    public int PhysicalDefense
    {
        get
        {
            return _charAttributes.GetBasePhysicalDefense();
        }
        set;
    }
    
    
    public int MysticDefense
    {
        get
        {
            return _charAttributes.GetBaseMysticDefense();
        }
        set;
    }

    public int PhysicalArmor
    {
        get
        {
            return 0;
        }
        set;
    }

    public int MysticalArmor
    {
        get
        {
            return _charAttributes.GetMysticArmor();
        }
    }

    public int SocialDefense
    {
        get
        {
            return _charAttributes.GetBaseSocialDefense();
        }
        set;
    }

    public int UnconsciousRating
    {
        get => _charAttributes.GetUnconsciousnessRating();
    }

    public int DeathRating
    {
        get => _charAttributes.GetDeathRating();
    }

    public int RecoveryTests
    {
        get => _charAttributes.GetRecoveryTests();
    }

    public int WoundThreshold
    {
        get => _charAttributes.GetWoundThreshold();
    }

    public int Karma { get; set; }
    public int KarmaModifier { get; set; }
    public int MaxKarma { get; set; }
    
    //***************************************************************Functions***********************************************
    public void AddDiscipline(Discipline discipline)
    {
        if (_disciplines.Contains(discipline) || _disciplines.Count >= 4)
            return;
        _disciplines.Add(discipline);
    }

    public List<Discipline> GetDisciplines()
    {
        List<Discipline> newDisciplineList = new();
        foreach (Discipline discipline in _disciplines)
        {
            newDisciplineList.Add(new(discipline));
        }
        return newDisciplineList;
    }
    public int GetNumberOfDisciplines()
    {
        return _disciplines.Count;
    }
    
    public ref readonly List<Discipline> GetDiscipline()
    {
        return ref _disciplines;
    }

    public void AddNewOptionTalent(Talent talent, string discipline)
    {
        foreach (Discipline ds in _disciplines)
        {
            if (ds.DisciplineName == discipline)
            {
                ds.AddNewOptionalTalent(talent);
            }
        }
    }
    
}