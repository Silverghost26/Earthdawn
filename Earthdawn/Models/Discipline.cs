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
        public string PhysicalDefense { get; set; } = string.Empty;
        public string MysticalDefense { get; set; } = string.Empty;
        public string SocialDefense { get; set; } = string.Empty;
        public string PhysicalArmor { get; set; } = string.Empty;
        public string MysticalArmor { get; set; } = string.Empty;
        public string Karma { get; set; } = string.Empty;
        public string Initiative { get; set; } = string.Empty;
        public string Recovery { get; set; } = string.Empty;
        public string Special { get; set; } = string.Empty;
        public List<string> FreeTalents { get; set; } = new();
        public List<string> Talents { get; set; } = new();
    }

public class DisciplineDisplayCard
{
    public string Name 
    {
        get => _name ?? string.Empty;  
        set => _name = value; 
    }
    private string? _name;
    public Discipline Disciplines 
    {
        get => _discipline ?? new Discipline();
        set => _discipline = value;
    }
    private Discipline? _discipline;
    
    public string ImagePath
    {
        get => _imagePath ?? string.Empty;
        set
        {
            string _name = Regex.Replace(value, "[^a-zA-Z0-9]", String.Empty); // Remove whitespace from the name
            _name = _name.ToLower(); // Convert to lowercase
            _imagePath  = "avares://Earthdawn/Assets/Portraits/" + _name + "disciplineportrait.png";
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

        displayString = displayString + "\n\n";
        foreach (string ft in circle.FreeTalents)
        {
            displayString = displayString + "Free Talent: " + ft + "\n";
        }
        if (circle.PhysicalDefense != string.Empty)
        {
            displayString = displayString + "Physical Defense: " + circle.PhysicalDefense + "\n";
        }
        if (circle.MysticalDefense != string.Empty)
        {
            displayString = displayString + "Mystical Defense: " + circle.MysticalDefense + "\n";
        }

        if (circle.SocialDefense != string.Empty)
        {
            displayString = displayString + "Social Defense: " + circle.SocialDefense + "\n";
        }

        if (circle.PhysicalArmor != string.Empty)
        {
            displayString = displayString + "Physical Armor: " + circle.PhysicalArmor + "\n";
        }

        if (circle.MysticalArmor != string.Empty)
        {
            displayString = displayString + "Mystical Armor: " + circle.MysticalArmor + "\n";
        }

        if (circle.Karma != string.Empty)
        {
            displayString = displayString + "Karma: " + circle.Karma + "\n";
        }

        if (circle.Initiative != string.Empty)
        {
            displayString = displayString + "Initiative: " + circle.Initiative + "\n";
        }

        if (circle.Recovery != string.Empty)
        {
            displayString = displayString + "Recovery: " + circle.Recovery + "\n";
        }

        if (circle.Special != string.Empty)
        {
            displayString = displayString + "Special Ability: " + circle.Special + "\n";
        }
        return displayString;
        
    }
   
}

