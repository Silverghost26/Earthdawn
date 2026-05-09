using System;
using System.Collections.Generic;
using System.IO;
using Earthdawn.Data;

namespace Earthdawn.Models;

public class Attributes
{
    private Dictionary<string, int> _attributes;


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
    }

    public int Dexterity
    {
        get => _attributes["Dexterity"];
        set
        {
            if (value >= 0)
            {
                _attributes["Dexterity"] = value;
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
    