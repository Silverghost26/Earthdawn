
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Earthdawn.Data;

namespace Earthdawn.Models;

public class  CharacterSheet : INotifyPropertyChanged
{

    private Attributes? _charAttributes;
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public CharacterSheet()
    {
        _charAttributes = new Attributes();
    }

    public CharacterSheet(Race race)
    {
        _charAttributes = new Attributes(race);
        Karma = race.KarmaMod;
    }
    
    
    //Properties
    public string CharacterName {
         get => _characterName ?? string.Empty;
         set => _characterName = value.Trim(); 
         }
    private string? _characterName;

    public string Discipline 
    {
        get => _disciplineName ?? string.Empty;
        set => _disciplineName = value.Trim();
    }
    private string? _disciplineName;

    public string Race
    {
        get => _race ?? string.Empty;
        set => _race = value.Trim();
    }
    private string? _race;

    public string Sex
    {
        get => _sex ?? string.Empty;
        set => _sex = value.Trim();
    }   
    private string? _sex;

    public int Age
    {
        get => _age;
        set => _age = value < 0 ? 0 : value; // Ensure Age is non-negative
    }
    private int _age;

    public string Height
    {
        get => _height ?? string.Empty;
        set => _height = value.Trim();
    }
    private string? _height;


    public string Weight
    {
        get => _weight ?? string.Empty;
        set => _weight = value.Trim();
    }       
    private string? _weight;

    public string HairColor
    {
        get => _haircolor ?? string.Empty;
        set => _haircolor = value.Trim();
    }   
    private string? _haircolor;

    public string EyeColor
    {
        get => _eyecolor ?? string.Empty;
        set => _eyecolor = value.Trim();
    }
    private string? _eyecolor;

    public string SkinColor
    {
        get => _skincolor ?? string.Empty;
        set => _skincolor = value.Trim();
    }   
    private string? _skincolor;

    public int Circle
    {
        get => _circle;
        set => _circle = value < 1 ? 1 : value; // Ensure Circle is at least 1  
    }
    private int _circle;

    public int MovementRate
    {
        get => _movementRate;
        set => _movementRate = value < 0 ? 0 : value; // Ensure Movement Rate is non-negative
    }
    private int _movementRate;

    public int FlyingMovementRate
    {
        get => _flyingMovementRate;
        set => _flyingMovementRate = value <= 0 ? 0 : value;
    }

    private int _flyingMovementRate;

    public int CarryingCapacity
    {
        get => _carryingCapacity;
        set => _carryingCapacity = value < 0 ? 0 : value; // Ensure Carrying Capacity is non-negative
    }   
    private int _carryingCapacity;

    public int LegendPointsTotal
    {
        get => _legendPointsTotal;
        private set => _legendPointsTotal = value < 0 ? 0 : value; // Ensure Legend Points is non-negative
    }
    private int _legendPointsTotal;

    public int LegendPointsAvailable
    {
        get => _legendPointsAvailable;
        private set => _legendPointsAvailable = value < 0 ? 0 : value; // Ensure Legend Points is non-negative
    }
    private int _legendPointsAvailable;

    public string LegendaryStatus
    {
        get => _legendaryStatus ?? string.Empty;
        set => _legendaryStatus = value.Trim();
    }
    private string? _legendaryStatus;
    
    public string Renown
    {
        get => _renown ?? string.Empty;
        set => _renown = value.Trim();
    }   
    private string? _renown;

    public string Reputation
    {
        get => _reputation ?? string.Empty;
        set => _reputation = value.Trim();
    }       
    private string? _reputation;

    public int Dexterity
    {
        get
        {
            return _charAttributes.Dexterity;
        } 
        set;
    }

    public int Strength
    {
        get
        {
            return _charAttributes.Strength;
        }
        set;
    }
    public int Toughness {
        get
        {
            return _charAttributes.Toughness;
        }
        set; 
    }

    public int Perception
    {
        get
        {
            return _charAttributes.Perception;
        }
        set;
    }

    public int Willpower
    {
        get
        {
            return _charAttributes.Willpower;
        } 
        set;
    }

    public int Charisma
    {
        get
        {
            return _charAttributes.Charisma;
        } 
        set;
    }
    public int Initiative {
        get
        {
            return _charAttributes.GetStepNumber(AttributesTypes.Dex);
        }
        set; 
    }
    private int _initiative;

    public int PhysicalDefense
    {
        get
        {
            return _charAttributes.GetBasePhysicalDefense();
        }
        set;
    }

    private int _physicalDefense;
    
    public int MysticDefense
    {
        get
        {
            return _charAttributes.GetBaseMysticDefense();
        }
        set;
    }
    private int _mysticDefense;

    public int PhysicalArmor
    {
        get
        {
            return 0;
        }
        set;
    }

    private int _physicalArmor;

    public int MysticalArmor
    {
        get
        {
            return _charAttributes.GetMysticArmor();
        }
    }

    private int _mysticalArmor;

    public int SocialDefense
    {
        get
        {
            return _charAttributes.GetBaseSocialDefense();
        }
        set;
    }

    private int _socialDefense;

    public int UnconsciousRating
    {
        get => _charAttributes.GetUnconsciousnessRating();
    }

    public int DeathRating
    {
        get => _charAttributes.GetDeathRating();
    }

    public int RecoveryTests
    {
        get => _charAttributes.GetRecoveryTests();
    }

    public int WoundThreshold
    {
        get => _charAttributes.GetWoundThreshold();
    }

    public int Karma { get; set; }
    public int KarmaModifier { get; set; }
    public int MaxKarma { get; set; }

    public List<SpecialAbility> RacialAbilities
    {
        get => _racialAbilities ??= new List<SpecialAbility>();
        set => _racialAbilities = value ?? new List<SpecialAbility>();
    }
    private List<SpecialAbility> _racialAbilities;

    public Discipline CharacterDiscipline
    {
        get => _characterDiscipline ??= new();
        set => _characterDiscipline = value;
    }
    private Discipline _characterDiscipline;
    
    public void AddRaceBaseAttributes(Race race)
    {
        Attributes attributes = new Attributes(race);
        _charAttributes = attributes;
    }
}