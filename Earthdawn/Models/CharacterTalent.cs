using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Earthdawn.Data;

namespace Earthdawn.Models;

public class CharacterTalent : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    public CharacterTalent(string talentName, Talent talent, DisciplineTiers dt, bool free, bool optional)
    {
        TalentName = talentName;
        Action = talent.Action;
        Strain = talent.Strain;
        Description = talent.Description;
        Attribute = ConvertStringToAttribute(talent.Step);
        TierReceived = dt;
        IsFreeTalent = free;
        IsOptionalTalent = optional;
    }
    public string TalentName { get; init; }
    public AttributesTypes Attribute { get; init; }
    public string Action { get; init; }
    public int Strain { get; init; }
    public string Description { get; init; }
    public DisciplineTiers TierReceived { get; init; }
    public bool IsFreeTalent { get; init; }
    public bool IsOptionalTalent { get; init; }

    public int Step
    {
        get => _step;
        private set
        {
            _step = value;
            OnPropertyChanged();
        }
    }
    private int _step;

    public int Rank
    {
        get => _rank;
        private set
        {
            _rank = value;
            OnPropertyChanged();
        }
    }
    private int _rank;
    
    //*****************************************Functions***********************************************************

    public void IncrementRank(Attributes att)
    {
        Rank += 1;
        Step = Rank + att.GetStepNumber(Attribute);
    }

    public void DecrementRank()
    {
        if (Rank > 0)
        {
            Rank -= 1;
            Step -= 1;
        }
    }

    public void UpdateRank(int rankChange, Attributes att)
    {
        Rank += rankChange;
        Step = Rank + att.GetStepNumber(Attribute);
    }

    private AttributesTypes ConvertStringToAttribute(string att)
    {
        if (att.ToLower() == "dex" || att.ToLower() == "dexterity")
            return AttributesTypes.Dex;
        if (att.ToLower() == "str" || att.ToLower() == "strength")
            return AttributesTypes.Str;
        if (att.ToLower() == "tou" || att.ToLower() == "toughness")
            return AttributesTypes.Tou;
        if (att.ToLower() == "per" || att.ToLower() == "perception")
            return AttributesTypes.Per;
        if (att.ToLower() == "wil" || att.ToLower() == "willpower")
            return AttributesTypes.Wil;
        if (att.ToLower() == "cha" || att.ToLower() == "charisma" || att.ToLower() == "chr")
            return AttributesTypes.Chr;
        return AttributesTypes.None;
    }
}