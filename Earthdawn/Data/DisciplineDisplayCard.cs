using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Earthdawn.Data;

public class DisciplineDisplayCard
{
    public DisciplineDisplayCard()
    {
        
    }
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
    public DisciplineData Disciplines 
    {
        get => _discipline ?? new DisciplineData();
        set => _discipline = value;
    }
    private DisciplineData? _discipline;
    
    
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

    private string CreateCircleDisplayString(DisciplineCircle disciplineCircle)
    {
        string displayString = "Talents: ";
        int index = 1;
        foreach (string t in disciplineCircle.Talents)
        {
            displayString = displayString + t;
            if (index < disciplineCircle.Talents.Count)
                displayString+= ", ";
            else
            {
                displayString += ".";
            }

            index++;
        }

        displayString = displayString;
        foreach (string ft in disciplineCircle.FreeTalents)
        {
            if(ft != string.Empty)
            {
                displayString = displayString + "\nFree Talent: " + ft;
            }
        }
        if (disciplineCircle.PhysicalDefense != 0)
        {
            displayString = displayString + "\nPhysical Defense: " + disciplineCircle.PhysicalDefense;
        }
        if (disciplineCircle.MysticalDefense != 0)
        {
            displayString = displayString + "\nMystical Defense: " + disciplineCircle.MysticalDefense;
        }

        if (disciplineCircle.SocialDefense != 0)
        {
            displayString = displayString + "\nSocial Defense: " + disciplineCircle.SocialDefense;
        }

        if (disciplineCircle.PhysicalArmor != 0)
        {
            displayString = displayString + "\nPhysical Armor: " + disciplineCircle.PhysicalArmor;
        }

        if (disciplineCircle.MysticalArmor != 0)
        {
            displayString = displayString + "\nMystical Armor: " + disciplineCircle.MysticalArmor;
        }

        if (disciplineCircle.Karma != string.Empty)
        {
            displayString = displayString + "\nKarma: " + disciplineCircle.Karma;
        }

        if (disciplineCircle.Initiative != 0)
        {
            displayString = displayString + "\nInitiative: " + disciplineCircle.Initiative;
        }

        if (disciplineCircle.Recovery != 0)
        {
            displayString = displayString + "\nRecovery: " + disciplineCircle.Recovery;
        }

        if (disciplineCircle.Special != string.Empty)
        {
            displayString = displayString + "\nSpecial Ability: " + disciplineCircle.Special;
        }
        return displayString;
        
    }
   
}

