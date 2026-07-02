
using System;
using System.Collections.Generic;
using Earthdawn.Data;
using EarthDawn.Services;

namespace Earthdawn.Models;

public class  CharacterCreationSheet : CharacterBase
{
    private int _racialDex;
    private int _racialStr;
    private int _racialTou;
    private int _racialPer;
    private int _racialWil;
    private int _racialChr;
    
    private List<string> _optionalTalensList;
    private int _spellPoints;
    public CharacterCreationSheet()
    {
        RemainingAttributePoints = 25;
        RemainingTalentPoints = 8;
        RemainingGeneralSkillPoints = 8;
        RemainingKnowledgeSkillPoints = 2;
        _optionalTalensList = new();
        _spellPoints = 0;
    }
    
   
    //Properties
    public int RemainingAttributePoints
    {
        get => _remainingAttributePoints;
        set
        {
            _remainingAttributePoints = value;
            Karma = _remainingAttributePoints + KarmaModifier;
        }
    }
    private int _remainingAttributePoints;

    public int RemainingTalentPoints { get; set; }
    public int RemainingGeneralSkillPoints { get; set; }
    public int RemainingKnowledgeSkillPoints { get; set; }
    public int SpellPoints
    {
        get => _spellPoints;
    }
    
    
    //*************************************************Functions*********************************************
    public void AddDiscipline(DisciplineDisplayCard card)
    {
        IDataServices dataService = new DataServices();
        var talents = dataService.LoadTalents();
        Discipline newDiscipline = new Discipline();
        newDiscipline.DisciplineName = card.Name;
        newDiscipline.DisciplineCircleLevel = 1;
        newDiscipline.DisciplinePrecedence = 1;
        int.TryParse(card.Disciplines.Durability, out int durability);
        newDiscipline.Durability = durability;
        newDiscipline.PhysicalDefenseBonus += card.Disciplines.Circles["First"].PhysicalDefense;
        newDiscipline.MysticalDefenseBonus += card.Disciplines.Circles["First"].MysticalDefense;
        newDiscipline.SocialDefenseBonus += card.Disciplines.Circles["First"].SocialDefense;
        newDiscipline.PhysicalArmorBonus += card.Disciplines.Circles["First"].PhysicalArmor;
        newDiscipline.MysticalArmorBonus += card.Disciplines.Circles["First"].MysticalArmor;
        newDiscipline.InitiativeBonus += card.Disciplines.Circles["First"].Initiative;
        newDiscipline.RecoveryTestBonus += card.Disciplines.Circles["First"].Recovery;
        newDiscipline.AddNewKarmaSpecial(card.Disciplines.Circles["First"].Karma);
        newDiscipline.AddNewCircleSpecial(card.Disciplines.Circles["First"].Special);
        if (card.Disciplines.Circles["First"].FreeTalents != null)
        {
            foreach (string talent in card.Disciplines.Circles["First"].FreeTalents)
            {
                if (!string.IsNullOrEmpty(talent))
                {
                    string tempTalent = string.Empty;
                    if (talent.Contains("Thread Weaving"))
                    {
                        tempTalent = "Thread Weaving";
                    }
                    else
                    {
                        tempTalent = talent;
                    }

                    var newTalent = talents[tempTalent];
                    newTalent.Name = talent;
                    newTalent.CircleObtained = 1;
                    newTalent.Rank = 1;
                    newDiscipline.AddNewFreeTalent(newTalent);
                }
            }
        }

        if (card.Disciplines.Circles["First"].Talents != null)
        {
            foreach (string talent in card.Disciplines.Circles["First"].Talents)
            {
                if (!string.IsNullOrEmpty(talent))
                {
                    string tempTalent = string.Empty;
                    if (talent.Contains("Thread Weaving"))
                    {
                        tempTalent = "Thread Weaving";
                    }
                    else
                    {
                        tempTalent = talent;
                    }

                    var newTalent = talents[tempTalent];
                    newTalent.Name = talent;
                    newTalent.CircleObtained = 1;
                    newDiscipline.AddNewTalent(newTalent);
                }
            }
        }
        _disciplines.Add(newDiscipline);
    }
    
    public void AddRaceBaseAttributes(Race race)
    {
        Karma = race.KarmaMod + _remainingAttributePoints;
        this.SetRacialAttributes(race); ;
        this.MovementRate = race.Movement;
        this.FlyingMovementRate = race.FlyingMovement;
        _racialChr = race.CHA;
        _racialDex = race.DEX;
        _racialPer = race.PER;
        _racialStr = race.STR;
        _racialTou = race.TOU;
        _racialWil = race.WIL;
    }
    
    private void SetRacialAttributes(Race race)
    {
        SetCharAttributes(new Attributes(race));
    }
    
    public List<string> GetTalentNameList()
    {
        List<string> characterTalents = new();
        foreach( var talent in _disciplines[0].GetDisciplineTalents())
        {
            characterTalents.Add(talent.Name);
        }
        return characterTalents;
    }

    public void AddNewSpell(Spell spell)
    {
        if (_spellPoints < spell.Circle)
            return;
        if (_disciplines[0].AddNewSpell(spell))
        {
            _spellPoints -= spell.Circle;
        }
    }

    public void RemoveSpell(Spell spell)
    {
        if (_disciplines[0].RemoveSpell(spell))
        {
            _spellPoints += spell.Circle;
        }
    }
    
    //Note: Free talents can not be upgraded with Attribute points or Legendpoints, they are tied to the Circle.
    public List<string> GetFreeTalentNameList()
    {
        List<string> characterFreeTalents = new();
        foreach (var talent in _disciplines[0].GetDisciplineFreeTalents())
        {
            characterFreeTalents.Add(talent.Name);
        }
    
        return characterFreeTalents;
    }

    public void IncrementTalent(string talentName)
    {
        if (RemainingTalentPoints > 0)
        {
            foreach (Talent ot in _disciplines[0].GetDisciplineOptionalTalents())
            {
                if (ot.Name == talentName && ot.Rank < 3)
                {
                    ot.Rank += 1;
                    RemainingTalentPoints -= 1;
                    return;
                }
            }
            foreach (Talent talent in _disciplines[0].GetDisciplineTalents())
            {
                if (talentName == talent.Name && talent.Rank < 3)
                {
                    talent.Rank += 1;
                    RemainingTalentPoints -= 1;
                    return;
                }
            }
        }
    }
    
    public void DecrementTalent(string talentName)
    {
        foreach (Talent ot in _disciplines[0].GetDisciplineOptionalTalents())
        {
            if (ot.Name == talentName && ot.Rank > 0)
            {
                ot.Rank -= 1;
                RemainingTalentPoints += 1;
                return;
            }
        }
        foreach (Talent talent in _disciplines[0].GetDisciplineTalents())
        {
            if (talentName == talent.Name && talent.Rank > 0)
            {
                talent.Rank -= 1;
                RemainingTalentPoints += 1;
            }
        }
    }

    public int AddOptionalTalent(Talent talent, string currentOptionalTalent = "")
    {
        int refund = 0;
        if (!string.IsNullOrEmpty(currentOptionalTalent))
        {
            refund = _disciplines[0].RemoveOptionalTalent(currentOptionalTalent);
        }
        _disciplines[0].AddNewOptionalTalent(talent);
        RemainingTalentPoints += refund;
        return refund;
    }
    

    public void IncrementAttribute(AttributesTypes att)
    {
        int cost = 0;
        switch (att)
        {
            case AttributesTypes.Chr:
                cost = GetAttributeIncreaseCostChr();
                if (cost <= RemainingAttributePoints && (Charisma - _racialChr) < 8)
                {
                    RemainingAttributePoints -= cost;
                    Charisma += 1;
                }
                break;
            case AttributesTypes.Per:
                cost = GetAttributeIncreaseCostPer();
                if (cost <= RemainingAttributePoints && (Perception - _racialPer) < 8)
                {
                    RemainingAttributePoints -= cost;
                    Perception += 1;
                    _spellPoints = _charAttributes.GetStepNumber(AttributesTypes.Per);
                }
                break;
            case AttributesTypes.Str:
                cost = GetAttributeIncreaseCostStr();
                if (cost <= RemainingAttributePoints && (Strength - _racialStr) < 8)
                {
                    RemainingAttributePoints -= cost;
                    Strength += 1;
                }
                break;
            case AttributesTypes.Tou:
                cost = GetAttributeIncreaseCostTou();
                if (cost <= RemainingAttributePoints && (Toughness - _racialTou) < 8)
                {
                    RemainingAttributePoints -= cost;
                    Toughness += 1;
                }
                break;
            case AttributesTypes.Wil:
                cost = GetAttributeIncreaseCostWil();
                if (cost <= RemainingAttributePoints && (Willpower - _racialWil) < 8)
                {
                    RemainingAttributePoints -= cost;
                    Willpower += 1;
                }
                break;
            case AttributesTypes.Dex:
                cost = GetAttributeIncreaseCostDex();
                if (cost <= RemainingAttributePoints && (Dexterity - _racialDex) < 8)
                {
                    RemainingAttributePoints -= cost;
                    Dexterity += 1;
                }
                break;
        }
    }
    
    public void DecrementAttribute(AttributesTypes att)
    {
        int cost = 0;
        switch (att)
        {
            case AttributesTypes.Chr:
                cost = GetAttributeDecrementCostChr();
                if ((Charisma - _racialChr) > -2)
                {
                    RemainingAttributePoints += cost;
                    Charisma -= 1;
                }
                break;
            case AttributesTypes.Per:
                cost = GetAttributeDecrementCostPer();
                if ((Perception - _racialPer) > -2)
                {
                    RemainingAttributePoints += cost;
                    Perception -= 1;
                    // OnPropertyChanged(nameof(RemainingAttributePoints));
                }
                break;
            case AttributesTypes.Str:
                cost = GetAttributeDecrementCostStr();
                if ((Strength - _racialStr) > -2)
                {
                    RemainingAttributePoints += cost;
                    Strength -= 1;
                    // OnPropertyChanged(nameof(RemainingAttributePoints));
                }
                break;
            case AttributesTypes.Tou:
                cost = GetAttributeDecrementCostTou();
                if ((Toughness - _racialTou) > -2)
                {
                    RemainingAttributePoints += cost;
                    Toughness -= 1;
                    // OnPropertyChanged(nameof(RemainingAttributePoints));
                }
                break;
            case AttributesTypes.Wil:
                cost = GetAttributeDecrementCostWil();
                if ((Willpower - _racialWil) > -2)
                {
                    RemainingAttributePoints += cost;
                    Willpower -= 1;
                    // OnPropertyChanged(nameof(RemainingAttributePoints));
                }
                break;
            case AttributesTypes.Dex:
                cost = GetAttributeDecrementCostDex();
                if ((Dexterity - _racialDex) > -2)
                {
                    RemainingAttributePoints += cost;
                    Dexterity -= 1;
                    // OnPropertyChanged(nameof(RemainingAttributePoints));
                }
                break;
        }
    }
    
    public int GetAttributeIncreaseCostDex()
    {
        return Math.Abs(CalculateAttributePointChange(Dexterity - _racialDex + 1));
    }
    public int GetAttributeIncreaseCostStr()
    {
        return Math.Abs(CalculateAttributePointChange(Strength - _racialStr + 1));
    }
    public int GetAttributeIncreaseCostTou()
    {
        return Math.Abs(CalculateAttributePointChange(Toughness - _racialTou + 1));
    }
    public int GetAttributeIncreaseCostPer()
    {
        return Math.Abs(CalculateAttributePointChange(Perception - _racialPer + 1));
    }
    public int GetAttributeIncreaseCostWil()
    {
        return Math.Abs(CalculateAttributePointChange(Willpower - _racialWil + 1));
    }
    public int GetAttributeIncreaseCostChr()
    {
        return Math.Abs(CalculateAttributePointChange(Charisma - _racialChr + 1));
    }
    public int GetAttributeDecrementCostDex()
    {
        return Math.Abs(CalculateAttributePointChange(Dexterity - _racialDex));
    }
    
    public int GetAttributeDecrementCostStr()
    {
        return Math.Abs(CalculateAttributePointChange(Strength - _racialStr));
    }
    public int GetAttributeDecrementCostTou()
    {
        return Math.Abs(CalculateAttributePointChange(Toughness - _racialTou));
    }
    public int GetAttributeDecrementCostPer()
    {
        return Math.Abs(CalculateAttributePointChange(Perception - _racialPer));
    }
    public int GetAttributeDecrementCostWil()
    {
        return Math.Abs(CalculateAttributePointChange(Willpower - _racialWil));
    }
    public int GetAttributeDecrementCostChr()
    {
        return Math.Abs(CalculateAttributePointChange(Charisma - _racialChr));
    }
    
    private int CalculateAttributePointChange(int attributeDiff)
    {
        //Cost in Attribuate points.
        //1,2,3 -1; 4,5,6 -2; 7,8 -3;
        //-1,-2, +1;
        if (attributeDiff <=0  && attributeDiff > -2)
        {
            return 1;
        }
        else if (attributeDiff > 8 || attributeDiff <= -2)
        {
            return 0;
        }
        else if (attributeDiff <= 3)
        {
            return -1;
        }
        else if (attributeDiff <= 6)
        {
            return -2;
        }
        else
        {
            return -3;
        }
    }

}