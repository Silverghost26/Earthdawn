using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Earthdawn.Models;
public class Discipline
    {
        public string Description { get; set; } = string.Empty;
        public string ImportantAttributes { get; set; } = string.Empty;
        public string HalfMagic { get; set; } = string.Empty;
        public string Durability { get; set; } = string.Empty;
        public Dictionary<string, List<string>> TalentOptions { get; set; } = new();
        public Dictionary<string, Circle> Circles { get; set; } = new();
    }

    public class Circle
    {
        public int PhysicalDefense { get; set; }
        public int MysticalDefense { get; set; } 
        public int SocialDefense { get; set; } 
        public int MysticalArmor { get; set; } 
        public int PhysicalArmor { get; set; } 
        public string Karma { get; set; } = string.Empty;
        public int Initiative { get; set; } 
        public int Recovery { get; set; } 
        public string Special { get; set; } = string.Empty;
        public List<string> FreeTalents { get; set; } = new();
        public List<string> Talents { get; set; } = new();
    }

public class DisciplineDisplayCard
{
    public string Name 
    {
        get => _name ?? string.Empty;
        set
        {
            _name = value;
            ImagePath = _name;
        }
        
    }
    private string? _name;
    public Discipline Disciplines 
    {
        get => _discipline ?? new Discipline();
        set => _discipline = value;
    }
    private Discipline? _discipline;
    
    
    // ************************************* UI Display Helpers ***************************************
    public string ImagePath
    {
        get => _imagePath ?? string.Empty;
        set
        {
            string _name = Regex.Replace(value, "[^a-zA-Z0-9]", String.Empty); // Remove whitespace from the name
            _name = _name.ToLower(); // Convert to lowercase
            _imagePath  = "avares://Earthdawn/Assets/Portraits/" + _name + "disciplineportrait.png";
            return;
        }
    }
    private string? _imagePath = string.Empty;
    
    public string? First { get;  set; } 
    public string? Second { get;  set; }
    public string? Third { get;  set; }
    public string? Fourth { get;  set; }
    public string? Fifth { get;  set; }
    public string? Sixth { get;  set; }
    public string? Seventh { get;  set; }
    public string? Eighth { get;  set; }
    public string? Ninth { get;  set; }
    public string? Tenth { get;  set; }
    public string? Eleventh { get;  set; }
    public string? Twelfth { get;  set; }
    public string? Thirteenth { get;  set; }
    public string? Fourteenth { get;  set; }
    public string? Fifteenth { get;  set; }
    
    public string? NoviceOptions
    {
        get => _noviceOptions;
        private set => _noviceOptions = value;
    }
    private string? _noviceOptions = string.Empty;
    public string? JourneymanOptions
    {
        get => _journeymanOptions;
        private set => _journeymanOptions = value;
    }
    private string?  _journeymanOptions = string.Empty;
    public string? WardenOptions
    {
        get => _wardenOptions;
        private set => _wardenOptions = value;
    }
    private string? _wardenOptions = string.Empty;
    public string? MasterOptions
    {
        get => _masterOptions;
        private set => _masterOptions = value;
    }
    private string? _masterOptions = string.Empty;

    public void SetDisplayForOptionalTalents()
    {
        int index = 1;
        foreach (string talent in _discipline.TalentOptions["Novice"])
        {
            if (index < _discipline.TalentOptions["Novice"].Count)
            {
                _noviceOptions += talent + ", ";
            }
            else
            {
                _noviceOptions += talent;
            }

            index++;
        }

        index = 1;
        foreach (string talent in _discipline.TalentOptions["Journeyman"])
        {
            if (index < _discipline.TalentOptions["Journeyman"].Count)
            {
                _journeymanOptions += talent + ", ";
            }
            else
            {
                _journeymanOptions += talent;
            }

            index++;
        }
        index = 1;
        
        foreach (string talent in _discipline.TalentOptions["Warden"])
        {
            if (index < _discipline.TalentOptions["Warden"].Count)
            {
                _wardenOptions += talent + ", ";
            }
            else
            {
                _wardenOptions += talent;
            }
            index++;
        }

        index = 1;
        foreach (string talent in _discipline.TalentOptions["Master"])
        {
            if (index < _discipline.TalentOptions["Master"].Count)
            {
                _masterOptions += talent + ", ";
            }
            else
            {
                _masterOptions += talent;
            }
            index++;
        }
    }

    public void SetPropertiesFromDictionary()
    {
        // We use the runtime type of the current object ('this')
        Type currentType = this.GetType();

        foreach (var kvp in Disciplines.Circles)
        {
            string propertyName = kvp.Key;
            string valueToSet = CreateCircleDisplayString(kvp.Value);

            //Use Reflection to get the property info
            PropertyInfo property = currentType.GetProperty(propertyName);

            if (property != null)
            {
                //Check if the property is writable before setting the value
                if (property.CanWrite)
                {
                    // Set the value on the current instance ('this')
                    property.SetValue(this, valueToSet);
                    Console.WriteLine($"SUCCESS: Set property '{propertyName}' to: {valueToSet ?? "NULL"}");
                }
            }
            else
            {
                throw new Exception($"Property '{propertyName}' not found on type '{currentType.Name}'");
            }

        }
    }

    private string CreateCircleDisplayString(Circle circle)
    {
        string displayString = "Talents: ";
        int index = 1;
        foreach (string t in circle.Talents)
        {
            displayString = displayString + t;
            if (index < circle.Talents.Count)
                displayString+= ", ";
            else
            {
                displayString += ".";
            }

            index++;
        }

        displayString = displayString;
        foreach (string ft in circle.FreeTalents)
        {
            if(ft != string.Empty)
            {
                displayString = displayString + "\nFree Talent: " + ft;
            }
        }
        if (circle.PhysicalDefense != 0)
        {
            displayString = displayString + "\nPhysical Defense: " + circle.PhysicalDefense;
        }
        if (circle.MysticalDefense != 0)
        {
            displayString = displayString + "\nMystical Defense: " + circle.MysticalDefense;
        }

        if (circle.SocialDefense != 0)
        {
            displayString = displayString + "\nSocial Defense: " + circle.SocialDefense;
        }

        if (circle.PhysicalArmor != 0)
        {
            displayString = displayString + "\nPhysical Armor: " + circle.PhysicalArmor;
        }

        if (circle.MysticalArmor != 0)
        {
            displayString = displayString + "\nMystical Armor: " + circle.MysticalArmor;
        }

        if (circle.Karma != string.Empty)
        {
            displayString = displayString + "\nKarma: " + circle.Karma;
        }

        if (circle.Initiative != 0)
        {
            displayString = displayString + "\nInitiative: " + circle.Initiative;
        }

        if (circle.Recovery != 0)
        {
            displayString = displayString + "\nRecovery: " + circle.Recovery;
        }

        if (circle.Special != string.Empty)
        {
            displayString = displayString + "\nSpecial Ability: " + circle.Special;
        }
        return displayString;
        
    }
   
}

