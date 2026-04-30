using System.Collections.Generic;

namespace Earthdawn.Models;

public class Attributes
{
    private Dictionary<string, AttributeInfo> _attributes;

    private class AttributeInfo
    {
        public int AttributeValue { get; set; }
        public int AttributeStep { get; set; }
    }

    public Attributes()
    {
        _attributes = new Dictionary<string, AttributeInfo>()
        {
            ["Dexterity"] = new AttributeInfo { AttributeValue = 1, AttributeStep = 1 },
            ["Strength"] = new AttributeInfo { AttributeValue = 1, AttributeStep = 1 },
            ["Toughness"] = new AttributeInfo { AttributeValue = 1, AttributeStep = 1 },
            ["Perception"] = new AttributeInfo { AttributeValue = 1, AttributeStep = 1 },
            ["Willpower"] = new AttributeInfo { AttributeValue = 1, AttributeStep = 1 },
            ["Charisma"] = new AttributeInfo { AttributeValue = 1, AttributeStep = 1 }
        };
    }

    public Attributes(Race race) : this()
    {
        _attributes["Dexterity"].AttributeValue = race.DEX;
        _attributes["Strength"].AttributeValue = race.STR;
        _attributes["Toughness"].AttributeValue = race.TOU;
        _attributes["Perception"].AttributeValue = race.PER;
        _attributes["Willpower"].AttributeValue = race.WIL;
        _attributes["Charisma"].AttributeValue = race.CHA;
    }
}