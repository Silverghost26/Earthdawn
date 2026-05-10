using System;
using System.Collections.Generic;

namespace Earthdawn.Models;

public class CharacterDefence
{
    
    private Attributes _attributes;
    private Armor _armor;
    private Shield _shield;
    private Discipline _discipline;
    private string _race;
    private List<TemporaryValues>? _tempPhysicalDefence;
    private List<TemporaryValues>? _tempMysticDefence;
    private List<TemporaryValues>? _tempSocialDefence;
    private List<TemporaryValues>? _tempPhysicalArmor;
    private List<TemporaryValues>? _tempMysticalArmor;
    
    public CharacterDefence(Attributes att, Armor armor, Shield shield, Discipline discipline, string race)
    {
        _attributes = att;
        _armor = armor;
        _shield = shield;
        _discipline = discipline;
        _race = race;
    }
    
    //Properties
    public int PhysicalDefence { get; set; }
    int _physicalDefence;
    public int MysticDefence { get; set; }
    private int _mysticDefence;
    public int SocialDefence { get; set; }
    private int _socialDefence;
    public int PhysicalArmor { get; set; }
    private int _physicalArmor;
    public int MysticalArmor { get; set; }
    private int _mysticalArmor;
    
    
    //Methods
    public void AddToPhysicalDefence(int value, string nameOfEffect = "Unknown Effect" , int durationInRounds = 0, bool permanent = false)
    {
        if (permanent)
        {
            PhysicalDefence += value;
        }
        else
        {
            if(_tempPhysicalDefence is null)
            {
                _tempPhysicalDefence = new();
            }
            _tempPhysicalDefence.Add(new TemporaryValues(nameOfEffect, value, durationInRounds));
        }
    }
    public void AddToMysticDefence(int value, string nameOfEffect = "Unknown Effect" , int durationInRounds = 0, bool permanent = false)
    {
        if (permanent)
        {
            MysticDefence += value;
        }
        else
        {
            if(_tempMysticDefence is null)
            {
                _tempMysticDefence = new();
            }
            _tempMysticDefence.Add(new TemporaryValues(nameOfEffect, value, durationInRounds));
        }
    }
    public void AddToSocialDefence(int value, string nameOfEffect = "Unknown Effect" , int durationInRounds = 0, bool permanent = false)
    {
        if (permanent)
        {
            SocialDefence += value;
        }
        else
        {
            if(_tempSocialDefence is null)
            {
                _tempSocialDefence = new();
            }
            _tempSocialDefence.Add(new TemporaryValues(nameOfEffect, value, durationInRounds));
        }
    }
    public void AddToPhysicalArmor(int value, string nameOfEffect = "Unknown Effect" , int durationInRounds = 0, bool permanent = false)
    {
        if (permanent)
        {
            PhysicalArmor += value;
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
            MysticalArmor += value;
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
    private int CalculatePhysicalDefence()
    {
        int modifier = 0;
        if (_tempPhysicalDefence is not null)
        {
            foreach (var effect in _tempPhysicalDefence)
            {
                modifier += effect.NumbOfPoints;
            }
        }

        return modifier;
    }

    private int CalculateMysticDefence()
    {
        int modifier = 0;
        if (_tempMysticDefence is not null)
        {
            foreach (var effect in _tempMysticDefence)
            {
                modifier += effect.NumbOfPoints;
            }
        }

        return modifier;
    }

    private int CalculateSocialDefense()
    {
        int modifier = 0;
        if (_tempSocialDefence is not null)
        {
            foreach (var effect in _tempSocialDefence)
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
}


