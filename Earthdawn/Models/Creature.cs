namespace Earthdawn.Models;
using System.Text.Json.Serialization;

public class Creature
{
    public string Name { get; set; } = string.Empty;
    public string Challenge { get; set; } = string.Empty;
    public int DEX { get; set; }
    public int STR { get; set; }
    public int TOU { get; set; }
    public int PER { get; set; }
    public int WIL { get; set; }
    public int CHA { get; set; }
    public int Initiative { get; set; }
    public int PhysicalDefense { get; set; }
    public int MysticDefense { get; set; }
    public int SocialDefense { get; set; }
    public int PhysicalArmor { get; set; }
    public int MysticArmor { get; set; }
    public int Unconsciousness { get; set; }
    public int DeathRating { get; set; }
    public int WoundThreshold { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Knockdown { get; set; }
    public int RecoveryTests { get; set; }
    public string Movement { get; set; } = string.Empty;
    public string Actions { get; set; } = string.Empty;
    public string Powers { get; set; } = string.Empty;
    public string SpecialManeuvers { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Loot { get; set; }

    // Default constructor
    public Creature()
    {
    }

    // Copy constructor
    public Creature(Creature other)
    {
        Name = other.Name;
        Challenge = other.Challenge;
        DEX = other.DEX;
        STR = other.STR;
        TOU = other.TOU;
        PER = other.PER;
        WIL = other.WIL;
        CHA = other.CHA;
        Initiative = other.Initiative;
        PhysicalDefense = other.PhysicalDefense;
        MysticDefense = other.MysticDefense;
        SocialDefense = other.SocialDefense;
        PhysicalArmor = other.PhysicalArmor;
        MysticArmor = other.MysticArmor;
        Unconsciousness = other.Unconsciousness;
        DeathRating = other.DeathRating;
        WoundThreshold = other.WoundThreshold;
        Knockdown = other.Knockdown;
        RecoveryTests = other.RecoveryTests;
        Movement = other.Movement;
        Actions = other.Actions;
        Powers = other.Powers;
        SpecialManeuvers = other.SpecialManeuvers;
        Description = other.Description;
        Loot = other.Loot;
    }
}