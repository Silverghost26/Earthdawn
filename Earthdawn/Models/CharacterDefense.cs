using System;
using System.Collections.Generic;

namespace Earthdawn.Models;

public class CharacterDefense
{
    
    private Attributes _attributes;
    private Armor _armor;
    private Shield _shield;
    private List<Discipline> _disciplines;
    private string _race;
    private List<TemporaryValues>? _tempPhysicalDefense;
    private List<TemporaryValues>? _tempMysticDefense;
    private List<TemporaryValues>? _tempSocialDefense;
    private List<TemporaryValues>? _tempPhysicalArmor;
    private List<TemporaryValues>? _tempMysticalArmor;
    
    public CharacterDefense(Attributes att, Armor armor, Shield shield, List<Discipline> disciplines, string race)
    {
        _attributes = att;
        _armor = armor;
        _shield = shield;
        _disciplines = disciplines;
        _race = race;
        _tempPhysicalArmor = new();
        _tempPhysicalDefense = new();
        _tempMysticalArmor = new();
        _tempMysticDefense = new();
        _tempSocialDefense = new();
    }

    public CharacterDefense(Attributes att) : this(att, new Armor(), new Shield(), new List<Discipline>(), "Unknown")
    {
    }

    public CharacterDefense(): this(new Attributes())
    {
    }

    public CharacterDefense(CharacterDefense characterDefense)
    {
        _tempPhysicalArmor = new();
        _tempPhysicalDefense = new();
        _tempMysticalArmor = new();
        _tempMysticDefense = new();
        _tempSocialDefense = new();
        _disciplines = new List<Discipline>();
        _attributes = new Attributes(characterDefense._attributes);
        _armor = new Armor(characterDefense._armor);
        _shield = new Shield(characterDefense._shield);
        foreach (Discipline disc in characterDefense._disciplines)
        {
            _disciplines.Add(new Discipline(disc));
        }
        foreach (TemporaryValues pd in characterDefense._tempPhysicalDefense)
        {
            _tempPhysicalDefense.Add(new TemporaryValues(pd.NameOfEffect, pd.NumbOfPoints, pd.EffectDurationInRounds));
        }

        foreach (TemporaryValues pa in characterDefense._tempPhysicalArmor)
        {
            _tempPhysicalArmor.Add(new TemporaryValues(pa.NameOfEffect, pa.NumbOfPoints, pa.EffectDurationInRounds));
        }

        foreach (TemporaryValues md in characterDefense._tempMysticDefense)
        {
            _tempMysticDefense.Add(new TemporaryValues(md.NameOfEffect, md.NumbOfPoints, md.EffectDurationInRounds));
        }
        foreach (TemporaryValues ma in characterDefense._tempMysticalArmor)
        {
            _tempMysticalArmor.Add(new TemporaryValues(ma.NameOfEffect, ma.NumbOfPoints, ma.EffectDurationInRounds));
        }

        foreach (TemporaryValues sd in characterDefense._tempSocialDefense)
        {
            _tempSocialDefense.Add(new TemporaryValues(sd.NameOfEffect, sd.NumbOfPoints, sd.EffectDurationInRounds));
        }
        
    }
    
    //Properties
    public int PhysicalDefense
    {
        get
        {
            return _physicalDefense + CalculatePhysicalDefense();
        } 
    }
    int _physicalDefense;

    public int MysticDefense
    {
        get
        {
            return _mysticDefense + CalculateMysticDefense();
        }
    }
    private int _mysticDefense;

    public int SocialDefense
    {
        get
        {
            return _socialDefense + CalculateSocialDefense();
        }
    }
    private int _socialDefense;

    public int PhysicalArmor
    {
        get
        {
            return _physicalArmor + CalculateSocialDefense();
        }
    }
    private int _physicalArmor;
    public int MysticalArmor
    {
        get
        {
            return _mysticalArmor + CalculateSocialDefense();
        }
    }
    private int _mysticalArmor;

    public int InitiativePenalty { get; private set; }

    //Methods
    public void AddToPhysicalDefense(int value, string nameOfEffect = "Unknown Effect" , int durationInRounds = 0, bool permanent = false)
    {
        if (permanent)
        {
            _physicalDefense += value;
        }
        else
        {
            if(_tempPhysicalDefense is null)
            {
                _tempPhysicalDefense = new();
            }
            _tempPhysicalDefense.Add(new TemporaryValues(nameOfEffect, value, durationInRounds));
        }
    }
    public void AddToMysticDefense(int value, string nameOfEffect = "Unknown Effect" , int durationInRounds = 0, bool permanent = false)
    {
        if (permanent)
        {
            _mysticDefense += value;
        }
        else
        {
            if(_tempMysticDefense is null)
            {
                _tempMysticDefense = new();
            }
            _tempMysticDefense.Add(new TemporaryValues(nameOfEffect, value, durationInRounds));
        }
    }
    public void AddToSocialDefense(int value, string nameOfEffect = "Unknown Effect" , int durationInRounds = 0, bool permanent = false)
    {
        if (permanent)
        {
            _socialDefense += value;
        }
        else
        {
            if(_tempSocialDefense is null)
            {
                _tempSocialDefense = new();
            }
            _tempSocialDefense.Add(new TemporaryValues(nameOfEffect, value, durationInRounds));
        }
    }
    public void AddToPhysicalArmor(int value, string nameOfEffect = "Unknown Effect" , int durationInRounds = 0, bool permanent = false)
    {
        if (permanent)
        {
            _physicalArmor += value;
        }
        else
        {
            if(_tempPhysicalArmor is null)
            {
                _tempPhysicalArmor = new();
            }
            _tempPhysicalArmor.Add(new TemporaryValues(nameOfEffect, value, durationInRounds));
        }
    }
    public void AddToMysticalArmor(int value, string nameOfEffect = "Unknown Effect" , int durationInRounds = 0, bool permanent = false)
    {
        if (permanent)
        {
            _mysticalArmor += value;
        }
        else
        {
            if(_tempMysticalArmor is null)
            {
                _tempMysticalArmor = new();
            }
            _tempMysticalArmor.Add(new TemporaryValues(nameOfEffect, value, durationInRounds));
        }
    }

    public void AddOrReplaceArmor(Armor armor)
    {
        _armor = armor;
        UpdateArmor();
    }

    public void RemoveArmor()
    {
        _armor = new Armor();
        UpdateArmor();
    }

    private void UpdateArmor()
    {
        SetDefense(DefenseType.MysticalArmor);
        SetDefense(DefenseType.PhysicalArmor);
    }

    public void AddOrReplaceShield(Shield shield)
    {
        _shield = shield;
        UpdateSheild();
    }

    public void RemoveShield()
    {
        _shield = new Shield();
        UpdateSheild();
    }

    private void UpdateSheild()
    {
        SetDefense(DefenseType.PhysicalDefense);
        SetDefense(DefenseType.MysticDefense);
    }

    private void SetDefense(DefenseType dt)
    {
        switch (dt)
        {
            case DefenseType.PhysicalDefense:
                _physicalDefense = _attributes.GetBasePhysicalDefense();
                _physicalDefense += _shield.PhysicalDefense;
                int physicalDefenseBonus = 0;
                foreach (Discipline discipline in _disciplines)
                {
                    if(discipline.PhysicalDefenseBonus > physicalDefenseBonus)
                        physicalDefenseBonus = discipline.PhysicalDefenseBonus;
                }
                _physicalDefense += physicalDefenseBonus;
                break;
            case DefenseType.MysticDefense:
                int mysticalDefenseBonus = 0;
                _mysticDefense = _attributes.GetBaseMysticDefense();
                _mysticDefense += _shield.MysticDefense;
                foreach (Discipline discipline in _disciplines)
                {
                    if (discipline.MysticalDefenseBonus > mysticalDefenseBonus)
                        mysticalDefenseBonus = discipline.MysticalDefenseBonus;
                }
                _mysticDefense += mysticalDefenseBonus;
                break;
            case DefenseType.SocialDefense:
                _socialDefense = _attributes.GetBaseSocialDefense();
                int socialDefenseBonus = 0;
                foreach (Discipline discipline in _disciplines)
                {
                    if (discipline.SocialDefenseBonus > socialDefenseBonus)
                        socialDefenseBonus = discipline.SocialDefenseBonus;
                }
                _socialDefense += socialDefenseBonus;
                break;
            case DefenseType.PhysicalArmor:
                _physicalArmor = _armor.PhysicalArmor;
                int physicalArmorBonus = 0;
                foreach (Discipline discipline in _disciplines)
                {
                    if (discipline.PhysicalArmorBonus > physicalArmorBonus)
                        physicalArmorBonus = discipline.PhysicalArmorBonus;
                }
                _physicalArmor += physicalArmorBonus;
                
                if (_race == "Obsidimen")
                    _physicalArmor += 3;
                break;
            case DefenseType.MysticalArmor:
                _mysticalArmor = _attributes.GetMysticArmor();
                _mysticalArmor += _armor.MysticArmor;
                int mysticalArmorBonus = 0;
                foreach (Discipline discipline in _disciplines)
                {
                    if (discipline.MysticalArmorBonus > mysticalArmorBonus)
                        mysticalArmorBonus = discipline.MysticalArmorBonus;
                }
                _mysticalArmor += mysticalArmorBonus;
                break;
        }
        InitiativePenalty = -_shield.InitiativePenalty;
        InitiativePenalty -= _armor.InitiativePenalty;
    }
    
    private int CalculatePhysicalDefense()
    {
        int modifier = 0;
        if (_tempPhysicalDefense is not null)
        {
            foreach (var effect in _tempPhysicalDefense)
            {
                modifier += effect.NumbOfPoints;
            }
        }

        return modifier;
    }

    private int CalculateMysticDefense()
    {
        int modifier = 0;
        if (_tempMysticDefense is not null)
        {
            foreach (var effect in _tempMysticDefense)
            {
                modifier += effect.NumbOfPoints;
            }
        }

        return modifier;
    }

    private int CalculateSocialDefense()
    {
        int modifier = 0;
        if (_tempSocialDefense is not null)
        {
            foreach (var effect in _tempSocialDefense)
            {
                modifier += effect.NumbOfPoints;
            }
        }

        return modifier;
    }

    private int CalculatePhysicalArmor()
    {
        int modifier = 0;
        if (_tempPhysicalArmor is not null)
        {
            foreach (var effect in _tempPhysicalArmor)
            {
                modifier += effect.NumbOfPoints;
            }
        }

        return modifier;
    }

    private int CalculateMysticalArmor()
    {
        int modifier = 0;
        if (_tempMysticalArmor is not null)
        {
            foreach (var effect in _tempMysticalArmor)
            {
                modifier += effect.NumbOfPoints;
            }
        }

        return modifier;
    }

    
    // Private Classes
    private class TemporaryValues
    {
        public int EffectDurationInRounds
        {
            get => _effectDurationInRounds;
            set => _effectDurationInRounds = value;
        }
        private int _effectDurationInRounds;

        public int NumbOfPoints
        {
            get => _numbOfPoints;
            set => _numbOfPoints = value;
        }
        private int _numbOfPoints;

        public string NameOfEffect
        {
            get => _nameOfEffect;
            set => _nameOfEffect = value ?? throw new ArgumentNullException(nameof(value));
        }
        private string _nameOfEffect;
        
        public TemporaryValues(string effectName, int numbOfPoints, int duration)
        {
            EffectDurationInRounds = duration;
            NumbOfPoints = numbOfPoints;
            NameOfEffect = effectName;
        }
    }
    private enum DefenseType
    {
        PhysicalDefense, 
        MysticDefense,
        SocialDefense,
        PhysicalArmor,
        MysticalArmor
    }
}




