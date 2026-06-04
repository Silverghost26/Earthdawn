using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Earthdawn.Data;

namespace Earthdawn.Models;

public class Attributes : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    private Dictionary<string, int> _attributes;
    private Dictionary<AttributesTypes, int> _legendPointIncrease;
    public Attributes()
    {
        _attributes = new Dictionary<string, int>
        {
            { "Dexterity", 10 },
            { "Strength", 1 },
            { "Toughness", 1 },
            { "Perception", 1 },
            { "Willpower", 1 },
            { "Charisma", 1 }
        };
        _legendPointIncrease = new Dictionary<AttributesTypes, int>
        {
            { AttributesTypes.Dex, 0 },
            { AttributesTypes.Str, 0 },
            { AttributesTypes.Per, 0 },
            { AttributesTypes.Tou, 0 },
            { AttributesTypes.Wil, 0 },
            { AttributesTypes.Chr, 0 }
        };
    }

    public Attributes(Race race)
    {
        _attributes = new Dictionary<string, int>
        {
            { "Dexterity", race.DEX },
            { "Strength", race.STR },
            { "Toughness", race.TOU },
            { "Perception", race.PER },
            { "Willpower", race.WIL },
            { "Charisma", race.CHA }
        };
        _legendPointIncrease = new Dictionary<AttributesTypes, int>
        {
            { AttributesTypes.Dex, 0 },
            { AttributesTypes.Str, 0 },
            { AttributesTypes.Per, 0 },
            { AttributesTypes.Tou, 0 },
            { AttributesTypes.Wil, 0 },
            { AttributesTypes.Chr, 0 }
        };
    }

    public Attributes(Attributes attributes)
    {
        _attributes = new Dictionary<string, int>
        {
            { "Dexterity", attributes.Dexterity },
            { "Strength", attributes.Strength },
            { "Toughness", attributes.Toughness },
            { "Perception", attributes.Perception },
            { "Willpower", attributes.Willpower },
            { "Charisma", attributes.Charisma }
        };
        _legendPointIncrease = new Dictionary<AttributesTypes, int>
        {
            { AttributesTypes.Dex, attributes.Dexterity },
            { AttributesTypes.Str, attributes.Strength },
            { AttributesTypes.Per, attributes.Perception },
            { AttributesTypes.Tou, attributes.Toughness },
            { AttributesTypes.Wil, attributes.Willpower },
            { AttributesTypes.Chr, attributes.Charisma }
        };
    }

    public int Dexterity
    {
        get => _attributes["Dexterity"];
        set
        {
            if (value >= 0)
            {
                _attributes["Dexterity"] = value;
                OnPropertyChanged();
            }
            else
            {
                throw new InvalidDataException("Value must be greater than 0");
            }
        }
    }

    public int Strength
    {
        get => _attributes["Strength"];
        set
        {
            if (value >= 0)
            {
                _attributes["Strength"] = value;
                OnPropertyChanged();
            }
            else
            {
                throw new InvalidDataException("Value must be greater than 0");
            }
        }
    }

    public int Toughness
    {
        get => _attributes["Toughness"];
        set
        {
            if (value >= 0)
            {
                _attributes["Toughness"] = value;
                OnPropertyChanged();
            }
            else
            {
                throw new InvalidDataException("Value must be greater than 0");
            }
        }
    }

    public int Perception
    {
        get => _attributes["Perception"];
        set
        {
            if (value >= 0)
            {
                _attributes["Perception"] = value;
                OnPropertyChanged();
            }
            else
            {
                throw new InvalidDataException("Value must be greater than 0");
            }
        }
    }

    public int Willpower
    {
        get => _attributes["Willpower"];
        set
        {
            if (value >= 0)
            {
                _attributes["Willpower"] = value;
                OnPropertyChanged();
            }
            else
            {
                throw new InvalidDataException("Value must be greater than 0");
            }
        }
    }

    public int Charisma
    {
        get => _attributes["Charisma"];
        set
        {
            if (value >= 0)
            {
                _attributes["Charisma"] = value;
                OnPropertyChanged();
            }
            else
            {
                throw new InvalidDataException("Value must be greater than 0");
            }
        }
    }

    public int GetStepNumber(AttributesTypes att)
    {
        string strValue = ConvertToString(att);
        if (string.IsNullOrEmpty(strValue) || strValue == "None")
            return 0;
        
        return ((int)Math.Ceiling(_attributes[strValue] / 3.0) + 1);
    }

    public int GetBasePhysicalDefense()
    {
        return ((int)Math.Ceiling(_attributes["Dexterity"] / 2.0) + 1);
    }

    public int GetBaseMysticDefense()
    {
        return ((int)Math.Ceiling(_attributes["Perception"] / 2.0) + 1);
    }
    
    public int GetBaseSocialDefense()
    {
        return ((int)Math.Ceiling(_attributes["Charisma"] / 2.0) + 1);
    }

    public int GetUnconsciousnessRating()
    {
        return _attributes["Toughness"] * 2;
    }

    public int GetDeathRating()
    {
        return GetUnconsciousnessRating() + GetStepNumber(AttributesTypes.Tou);
    }

    public int GetWoundThreshold()
    {
        return ((int)Math.Ceiling(_attributes["Toughness"] / 2.0) + 2);
    }

    public int GetRecoveryTests()
    {
        return ((int)Math.Ceiling(_attributes["Toughness"] / 6.0));
    }

    public int GetMysticArmor()
    {
        return ((int)Math.Floor(_attributes["Willpower"] / 5.0));
    }
    
    //*************************************Functions*******************************************
    public void AddLegendPointIncrease(AttributesTypes att)
    {
        if (_legendPointIncrease[att] < 3)
        {
            string strValue = ConvertToString(att);
            if (string.IsNullOrEmpty(strValue) || strValue == "None")
                return;
            _attributes[strValue] += 1;
            _legendPointIncrease[att] += 1;
        }
    }

    public bool LegendPointIncreaseAvailable(AttributesTypes att)
    {
        return _legendPointIncrease[att] < 3;
    }
    
    private string ConvertToString(AttributesTypes att)
    {
        string strValue = string.Empty;
        switch (att)
        {
            case AttributesTypes.Dex:
                strValue = "Dexterity";
                break;
            case AttributesTypes.Str:
                strValue = "Strength";
                break;
            case AttributesTypes.Per:
                strValue = "Perception";
                break;
            case AttributesTypes.Tou:
                strValue = "Toughness";
                break;
            case AttributesTypes.Wil:
                strValue = "Willpower";
                break;
            case AttributesTypes.Chr:
                strValue = "Charisma";
                break;
        }
        return strValue;
    }
}
    