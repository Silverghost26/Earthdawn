
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using Earthdawn.Data;

namespace Earthdawn.Models;

public class  CharacterCreationSheet : CharacterBase
{
    private List<string> _optionalTalensList;
    public CharacterCreationSheet()
    {
        RemainingAttributePoints = 25;
        RemainingTalentPoints = 8;
        RemainingGeneralSkillPoints = 8;
        RemainingKnowledgeSkillPoints = 2;
        _optionalTalensList = new();
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

    public void AddRaceBaseAttributes(Race race)
    {
        Karma = race.KarmaMod + _remainingAttributePoints;
        this.SetRacialAttributes(race); ;
        this.MovementRate = race.Movement;
        this.FlyingMovementRate = race.FlyingMovement;
    }

    public void AddOptionalDisciplineTalents(List<string> optionalTalents)
    {
        _optionalTalensList = optionalTalents;
    }
    
    public List<string> GetOptionalTalents()
    {
        return _optionalTalensList;
    }

    //Note: During character Creation there should only be one discipline, thus only one element in the list.
    public List<string> GetTalentNameList()
    {
        List<string> characterTalents = new();
        string firstDiscipline = this.GetAllCharacterDisciplines()[0];
        foreach( var talent in GetDisciplineCircleByName(firstDiscipline).Talents)
        {
            if(!talent.IsFreeTalent)
                characterTalents.Add(talent.TalentName);
        }
        return characterTalents;
    }
    
    //Note: Free talents can not be upgraded with Attribute points or Legendpoints, they are tied to the Circle.
    public List<string> GetFreeTalentNameList()
    {
        List<string> characterFreeTalents = new();
        string firstDiscipline = GetAllCharacterDisciplines()[0];
        foreach (var talent in GetDisciplineCircleByName(firstDiscipline).Talents)
        {
            if(talent.IsFreeTalent)
                characterFreeTalents.Add(talent.TalentName);
        }

        return characterFreeTalents;
    }

    public List<CharacterTalent> GetDisciplineTalentList()
    {
        List<CharacterTalent> disciplineTalents = new();
        if (_characterDisciplineCircles.Count == 0)
            return disciplineTalents;
            
        string firstDiscipline = GetAllCharacterDisciplines()[0];
        foreach (var talent in GetDisciplineCircleByName(firstDiscipline).Talents)
        {
            if (!talent.IsFreeTalent)
                disciplineTalents.Add(talent);
        }
        return disciplineTalents;
    }

    public List<CharacterTalent> GetFreeTalentList()
    {
        List<CharacterTalent> freeTalents = new();
        if (_characterDisciplineCircles.Count == 0)
            return freeTalents;
            
        string firstDiscipline = GetAllCharacterDisciplines()[0];
        foreach (var talent in GetDisciplineCircleByName(firstDiscipline).Talents)
        {
            if (talent.IsFreeTalent)
                freeTalents.Add(talent);
        }
        return freeTalents;
    }

    public List<CharacterTalent> GetTalentList()
    {
        return _characterDisciplineCircles[0].Talents;
    }

    private void SetRacialAttributes(Race race)
    {
        SetCharAttributes(new Attributes(race));
        SetStartingAttributes(new Attributes(race));
    }

    public void IncrementTalent(string talentName)
    {
        if (RemainingAttributePoints > 0)
        {
            string characterDiscipline = GetAllCharacterDisciplines()[0];
            DisciplineCircle dc = GetDisciplineCircleByName(characterDiscipline);
            if (dc != null)
            {
                foreach (CharacterTalent talent in dc.Talents)
                {
                    if (talentName == talent.TalentName && talent.Rank < 3)
                    {
                        talent.IncrementRank(_charAttributes);
                        RemainingAttributePoints -= 1;
                        OnPropertyChanged(nameof(RemainingAttributePoints));
                    }
                }
            }
        }
    }

    public void DecremenetTalent(string talentName)
    {
        string characterDiscipline = GetAllCharacterDisciplines()[0];
        DisciplineCircle dc = GetDisciplineCircleByName(characterDiscipline);
        if (dc != null)
        {
            foreach (CharacterTalent talent in dc.Talents)
            {
                if (talentName == talent.TalentName && talent.Rank > 0)
                {
                    talent.DecrementRank();
                    RemainingAttributePoints += 1;
                    OnPropertyChanged(nameof(RemainingAttributePoints));
                }
            }
        }
    }
    
    public void IncrementAttribute(AttributesTypes att)
    {
        int cost = 0;
        switch (att)
        {
            case AttributesTypes.Chr:
                cost = GetAttributeIncreaseCostChr();
                if (cost <= RemainingAttributePoints && (Charisma - OriginalChr) < 8)
                {
                    RemainingAttributePoints -= cost;
                    Charisma += 1;
                    OnPropertyChanged(nameof(RemainingAttributePoints));
                }
                break;
            case AttributesTypes.Per:
                cost = GetAttributeIncreaseCostPer();
                if (cost <= RemainingAttributePoints && (Perception - OriginalPer) < 8)
                {
                    RemainingAttributePoints -= cost;
                    Perception += 1;
                    OnPropertyChanged(nameof(RemainingAttributePoints));
                }
                break;
            case AttributesTypes.Str:
                cost = GetAttributeIncreaseCostStr();
                if (cost <= RemainingAttributePoints && (Strength - OriginalStr) < 8)
                {
                    RemainingAttributePoints -= cost;
                    Strength += 1;
                    OnPropertyChanged(nameof(RemainingAttributePoints));
                }
                break;
            case AttributesTypes.Tou:
                cost = GetAttributeIncreaseCostTou();
                if (cost <= RemainingAttributePoints && (Toughness - OriginalTou) < 8)
                {
                    RemainingAttributePoints -= cost;
                    Toughness += 1;
                    OnPropertyChanged(nameof(RemainingAttributePoints));
                }
                break;
            case AttributesTypes.Wil:
                cost = GetAttributeIncreaseCostWil();
                if (cost <= RemainingAttributePoints && (Willpower - OriginalWil) < 8)
                {
                    RemainingAttributePoints -= cost;
                    Willpower += 1;
                    OnPropertyChanged(nameof(RemainingAttributePoints));
                }
                break;
            case AttributesTypes.Dex:
                cost = GetAttributeIncreaseCostDex();
                if (cost <= RemainingAttributePoints && (Dexterity - OriginalDex) < 8)
                {
                    RemainingAttributePoints -= cost;
                    Dexterity += 1;
                    OnPropertyChanged(nameof(RemainingAttributePoints));
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
                if ((Charisma - OriginalChr) > -2)
                {
                    RemainingAttributePoints += cost;
                    Charisma -= 1;
                    OnPropertyChanged(nameof(RemainingAttributePoints));
                }
                break;
            case AttributesTypes.Per:
                cost = GetAttributeDecrementCostPer();
                if ((Perception - OriginalPer) > -2)
                {
                    RemainingAttributePoints += cost;
                    Perception -= 1;
                    OnPropertyChanged(nameof(RemainingAttributePoints));
                }
                break;
            case AttributesTypes.Str:
                cost = GetAttributeDecrementCostStr();
                if ((Strength - OriginalStr) > -2)
                {
                    RemainingAttributePoints += cost;
                    Strength -= 1;
                    OnPropertyChanged(nameof(RemainingAttributePoints));
                }
                break;
            case AttributesTypes.Tou:
                cost = GetAttributeDecrementCostTou();
                if ((Toughness - OriginalTou) > -2)
                {
                    RemainingAttributePoints += cost;
                    Toughness -= 1;
                    OnPropertyChanged(nameof(RemainingAttributePoints));
                }
                break;
            case AttributesTypes.Wil:
                cost = GetAttributeDecrementCostWil();
                if ((Willpower - OriginalWil) > -2)
                {
                    RemainingAttributePoints += cost;
                    Willpower -= 1;
                    OnPropertyChanged(nameof(RemainingAttributePoints));
                }
                break;
            case AttributesTypes.Dex:
                cost = GetAttributeDecrementCostDex();
                if ((Dexterity - OriginalDex) > -2)
                {
                    RemainingAttributePoints += cost;
                    Dexterity -= 1;
                    OnPropertyChanged(nameof(RemainingAttributePoints));
                }
                break;
        }
    }

    public int GetAttributeIncreaseCostDex()
    {
        return Math.Abs(CalculateAttributePointChange(Dexterity - OriginalDex + 1));
    }
    public int GetAttributeIncreaseCostStr()
    {
        return Math.Abs(CalculateAttributePointChange(Strength - OriginalStr + 1));
    }
    public int GetAttributeIncreaseCostTou()
    {
        return Math.Abs(CalculateAttributePointChange(Toughness - OriginalTou + 1));
    }
    public int GetAttributeIncreaseCostPer()
    {
        return Math.Abs(CalculateAttributePointChange(Perception - OriginalPer + 1));
    }
    public int GetAttributeIncreaseCostWil()
    {
        return Math.Abs(CalculateAttributePointChange(Willpower - OriginalWil + 1));
    }
    public int GetAttributeIncreaseCostChr()
    {
        return Math.Abs(CalculateAttributePointChange(Charisma - OriginalChr + 1));
    }
    public int GetAttributeDecrementCostDex()
    {
        return Math.Abs(CalculateAttributePointChange(Dexterity - OriginalDex));
    }

    public int GetAttributeDecrementCostStr()
    {
        return Math.Abs(CalculateAttributePointChange(Strength - OriginalStr));
    }
    public int GetAttributeDecrementCostTou()
    {
        return Math.Abs(CalculateAttributePointChange(Toughness - OriginalTou));
    }
    public int GetAttributeDecrementCostPer()
    {
        return Math.Abs(CalculateAttributePointChange(Perception - OriginalPer));
    }
    public int GetAttributeDecrementCostWil()
    {
        return Math.Abs(CalculateAttributePointChange(Willpower - OriginalWil));
    }
    public int GetAttributeDecrementCostChr()
    {
        return Math.Abs(CalculateAttributePointChange(Charisma - OriginalChr));
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