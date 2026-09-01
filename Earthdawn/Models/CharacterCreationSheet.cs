
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
    
    IDataServices dataService = new DataServices();
    private List<string> _optionalTalensList;
    private Dictionary<string, Talent> talents;
    private List<SkillDisplayCard> _skillDisplayCards;
    private List<Skill> _availableSkillList;
    private int _spellPoints;
    public CharacterCreationSheet()
    {
        _availableSkillList = new List<Skill>();
        RemainingAttributePoints = 25;
        RemainingTalentPoints = 8;
        RemainingGeneralSkillPoints = 8;
        RemainingKnowledgeSkillPoints = 2;
        RemainingArtisanSkillPoints = 1;
        RemainingSpeakLanguageSkillPoints = 2;
        RemainingReadWriteSkillPoints = 1;
        _optionalTalensList = new();
        _spellPoints = 0;
        Money = new Money
        {
            Silver = 200
        };
        talents = dataService.LoadTalents();
        CreateSkillList(dataService.LoadSkillsList());
    }

    public List<Skill> AvailableSkillList
    {
        get => _availableSkillList;
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
    public int RemainingReadWriteSkillPoints { get; set; }
    public int RemainingArtisanSkillPoints { get; set; }
    public int RemainingSpeakLanguageSkillPoints { get; set; }
    public int SpellPoints
    {
        get => _spellPoints;
    }
    
    
    //*************************************************Functions*********************************************
    private void CreateSkillList(List<SkillDisplayCard> skillDisplayCards)
    {
        foreach (SkillDisplayCard sdc in skillDisplayCards)
        {
            if (string.Equals(sdc.Skills.Tier, "novice", StringComparison.OrdinalIgnoreCase))
            {
                _availableSkillList.Add(sdc.Skills);
            }
        }

        foreach (string t in talents.Keys)
        {
            if (talents[t].SkillUse == "Yes" && talents[t].SkillLevel == "Novice")
            {
                _availableSkillList.Add(new Skill
                {
                    Action = talents[t].Action,
                    Description = talents[t].Description,
                    Name = t,
                    Rank = talents[t].Rank,
                    Step = talents[t].Step,
                    Strain = talents[t].Strain,
                    Tier = talents[t].SkillLevel
                });
            }
        }
    }
    public void AddDiscipline(DisciplineDisplayCard card)
    {
        //IDataServices dataService = new DataServices();
        //var talents = dataService.LoadTalents();
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
        _spellPoints = _charAttributes.GetStepNumber(AttributesTypes.Per);
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
                if (cost <= RemainingAttributePoints && (CharacterAttributes.Charisma - _racialChr) < 8)
                {
                    RemainingAttributePoints -= cost;
                    CharacterAttributes.Charisma += 1;
                }
                break;
            case AttributesTypes.Per:
                cost = GetAttributeIncreaseCostPer();
                if (cost <= RemainingAttributePoints && (CharacterAttributes.Perception - _racialPer) < 8)
                {
                    RemainingAttributePoints -= cost;
                    CharacterAttributes.Perception += 1;
                    _spellPoints = _charAttributes.GetStepNumber(AttributesTypes.Per);
                }
                break;
            case AttributesTypes.Str:
                cost = GetAttributeIncreaseCostStr();
                if (cost <= RemainingAttributePoints && (CharacterAttributes.Strength - _racialStr) < 8)
                {
                    RemainingAttributePoints -= cost;
                    CharacterAttributes.Strength += 1;
                }
                break;
            case AttributesTypes.Tou:
                cost = GetAttributeIncreaseCostTou();
                if (cost <= RemainingAttributePoints && (CharacterAttributes.Toughness - _racialTou) < 8)
                {
                    RemainingAttributePoints -= cost;
                    CharacterAttributes.Toughness += 1;
                }
                break;
            case AttributesTypes.Wil:
                cost = GetAttributeIncreaseCostWil();
                if (cost <= RemainingAttributePoints && (CharacterAttributes.Willpower - _racialWil) < 8)
                {
                    RemainingAttributePoints -= cost;
                    CharacterAttributes.Willpower += 1;
                }
                break;
            case AttributesTypes.Dex:
                cost = GetAttributeIncreaseCostDex();
                if (cost <= RemainingAttributePoints && (CharacterAttributes.Dexterity - _racialDex) < 8)
                {
                    RemainingAttributePoints -= cost;
                    CharacterAttributes.Dexterity += 1;
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
                if ((CharacterAttributes.Charisma - _racialChr) > -2)
                {
                    RemainingAttributePoints += cost;
                    CharacterAttributes.Charisma -= 1;
                }
                break;
            case AttributesTypes.Per:
                cost = GetAttributeDecrementCostPer();
                if ((CharacterAttributes.Perception - _racialPer) > -2)
                {
                    RemainingAttributePoints += cost;
                    CharacterAttributes.Perception -= 1;
                    // OnPropertyChanged(nameof(RemainingAttributePoints));
                }
                break;
            case AttributesTypes.Str:
                cost = GetAttributeDecrementCostStr();
                if ((CharacterAttributes.Strength - _racialStr) > -2)
                {
                    RemainingAttributePoints += cost;
                    CharacterAttributes.Strength -= 1;
                    // OnPropertyChanged(nameof(RemainingAttributePoints));
                }
                break;
            case AttributesTypes.Tou:
                cost = GetAttributeDecrementCostTou();
                if ((CharacterAttributes.Toughness - _racialTou) > -2)
                {
                    RemainingAttributePoints += cost;
                    CharacterAttributes.Toughness -= 1;
                    // OnPropertyChanged(nameof(RemainingAttributePoints));
                }
                break;
            case AttributesTypes.Wil:
                cost = GetAttributeDecrementCostWil();
                if ((CharacterAttributes.Willpower - _racialWil) > -2)
                {
                    RemainingAttributePoints += cost;
                    CharacterAttributes.Willpower -= 1;
                    // OnPropertyChanged(nameof(RemainingAttributePoints));
                }
                break;
            case AttributesTypes.Dex:
                cost = GetAttributeDecrementCostDex();
                if ((CharacterAttributes.Dexterity - _racialDex) > -2)
                {
                    RemainingAttributePoints += cost;
                    CharacterAttributes.Dexterity -= 1;
                    // OnPropertyChanged(nameof(RemainingAttributePoints));
                }
                break;
        }
    }
    
    public int GetAttributeIncreaseCostDex()
    {
        return Math.Abs(CalculateAttributePointChange(CharacterAttributes.Dexterity - _racialDex + 1));
    }
    public int GetAttributeIncreaseCostStr()
    {
        return Math.Abs(CalculateAttributePointChange(CharacterAttributes.Strength - _racialStr + 1));
    }
    public int GetAttributeIncreaseCostTou()
    {
        return Math.Abs(CalculateAttributePointChange(CharacterAttributes.Toughness - _racialTou + 1));
    }
    public int GetAttributeIncreaseCostPer()
    {
        return Math.Abs(CalculateAttributePointChange(CharacterAttributes.Perception - _racialPer + 1));
    }
    public int GetAttributeIncreaseCostWil()
    {
        return Math.Abs(CalculateAttributePointChange(CharacterAttributes.Willpower - _racialWil + 1));
    }
    public int GetAttributeIncreaseCostChr()
    {
        return Math.Abs(CalculateAttributePointChange(CharacterAttributes.Charisma - _racialChr + 1));
    }
    public int GetAttributeDecrementCostDex()
    {
        return Math.Abs(CalculateAttributePointChange(CharacterAttributes.Dexterity - _racialDex));
    }
    
    public int GetAttributeDecrementCostStr()
    {
        return Math.Abs(CalculateAttributePointChange(CharacterAttributes.Strength - _racialStr));
    }
    public int GetAttributeDecrementCostTou()
    {
        return Math.Abs(CalculateAttributePointChange(CharacterAttributes.Toughness - _racialTou));
    }
    public int GetAttributeDecrementCostPer()
    {
        return Math.Abs(CalculateAttributePointChange(CharacterAttributes.Perception - _racialPer));
    }
    public int GetAttributeDecrementCostWil()
    {
        return Math.Abs(CalculateAttributePointChange(CharacterAttributes.Willpower - _racialWil));
    }
    public int GetAttributeDecrementCostChr()
    {
        return Math.Abs(CalculateAttributePointChange(CharacterAttributes.Charisma - _racialChr));
    }

    public bool BuyItem(float cost)
    {
        if (cost <= Money.Silver)
        {
            if ((int)((cost % 1) * 10) == 0)
            {
                Money.Silver -= (int)cost;
            }
            else
            {
                float remainder = cost % 1;
                int copper = (int)(remainder * 10);

                if (Money.Copper >= copper)
                {
                    Money.Copper -= copper;
                }
                else
                {
                    Money.Silver -= ((int)Math.Floor(cost) + 1);
                    Money.Copper = ((Money.Copper + 10) - copper);
                }
            }
            return true;
        }
        return false;
    }

    public void ReturnItem(float cost)
    {
        if ((int)((cost % 1) * 10) == 0)
        {
            Money.Silver += (int)cost;
        }
        else
        {
            int copper = (int)((cost % 1) * 10);
            Money.Silver += (int)Math.Floor(cost);
            if ((Money.Copper + copper) >= 10)
            {
                Money.Silver += 1;
                Money.Copper = (Money.Copper + copper) - 10;
            }
            else
            {
                Money.Copper += copper;
            }
        }
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