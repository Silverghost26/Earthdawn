using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;

namespace Earthdawn.Models;

public class SpecialAbility
{
    public string Ability { get; set; }
    public string Description { get; set; }
}
public class Race
{
    // Racial Desciption
    public string Description { get; set; }

    //Starting Attributes
    public int DEX { get; set; }
    public int STR { get; set; }
    public int TOU { get; set; }
    public int PER { get; set; }
    public int WIL { get; set; }
    public int CHA { get; set; }

    // Movement Rates in yards per round
    public int Movement { get; set; }
    public int FlyingMovement { get; set; }

    //Racial Karma Modifier
    public int KarmaMod { get; set; }


    //Racial Special Abilities
    public List<SpecialAbility> Abilities 
    { 
        get => _abilities ?? new List<SpecialAbility>();
        set => _abilities = value;}
        private List<SpecialAbility>? _abilities;
    }

public class RaceDisplayCard
{
    public string? UpperAbilityText { get; set; }
    public string? LowerAbilityText { get; set; }
    
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

    public Race NameGiverRace
    {
        get => _nameGiverRace ?? new Race();
        init => _nameGiverRace = value;
    }
    private Race? _nameGiverRace;

    public string ImagePath
    {
        get => _imagePath ?? string.Empty;
        set
        {
            string _name = Regex.Replace(value, "[^a-zA-Z0-9]", String.Empty); // Remove whitespace from the name
            _name = _name.ToLower(); // Convert to lowercase
            _imagePath  = "avares://Earthdawn/Assets/Portraits/" + _name + "racialportrait.png";
        }
    }
    private string? _imagePath = string.Empty;

    public string RaceAbilities
    {
        get 
        {
            if (_raceAbilities == null)
            {
                string tempAbilities = string.Empty;
                foreach (var ability in NameGiverRace.Abilities)
                {
                    tempAbilities = tempAbilities + ability.Ability + "\n" + ability.Description + "\n\n";
                }
                _raceAbilities = tempAbilities;
            }
            return _raceAbilities;
        }
    }
    private string? _raceAbilities;
    
    public void CalculateAbilitiesStringBreakPoint()
    {
        string abilityDescription = this.RaceAbilities;
        int expectedCharPerLine = 135;
        int expectedNumberOfLines = 15;
        int maxLength = expectedCharPerLine * expectedNumberOfLines;
        int splitIndex = 0;
        // Don't exceed the end of the string.
        int searchIndex = Math.Min(maxLength, abilityDescription.Length);

        if (abilityDescription.Length > maxLength)
        {
            //find the number of new line characters in the upper substring
            splitIndex = abilityDescription.IndexOf(' ', searchIndex);
            int newLineCount = GetNewLineCount(abilityDescription.Substring(0, splitIndex));
            
            //If new line character are found update the expected Number of lines.
            if (newLineCount > 1)
            {
                searchIndex = searchIndex - (expectedCharPerLine * (newLineCount + 1));
            
                //Recalculate character count
                splitIndex = abilityDescription.IndexOf(' ', searchIndex);
            }
        
            //Set the observable strings.
            UpperAbilityText = abilityDescription.Substring(0, splitIndex);
            LowerAbilityText = abilityDescription.Substring(splitIndex + 1);
        }
        else
        {
            UpperAbilityText = abilityDescription;
            LowerAbilityText = string.Empty;
        }
    }

    private int GetNewLineCount(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return 0;
        }

        return Regex.Matches(text, @"\r?\n|\r").Count;
    }
}