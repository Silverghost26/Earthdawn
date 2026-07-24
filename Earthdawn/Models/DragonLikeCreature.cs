namespace Earthdawn.Models;

public class DragonLikeCreature : Creature
{
    public string Attacks { get; set; } = string.Empty;
    public string AdditionalPowers { get; set; } = string.Empty;
    public string Spells { get; set; } = string.Empty;
    public string Shapeshifting { get; set; } = string.Empty;
    public string NamegiverFormAbilities { get; set; } = string.Empty;
    public string AstralDetection { get; set; } = string.Empty;
    public string NamegiverFormPowers { get; set; } = string.Empty;
    public string Equipment { get; set; } = string.Empty;
    public string DragonBreathNote { get; set; } = string.Empty;
    public string HydraArmor { get; set; } = string.Empty;

    // Default constructor
    public DragonLikeCreature() : base()
    {
    }

    // Copy constructor
    public DragonLikeCreature(DragonLikeCreature other) : base(other)
    {
        Attacks = other.Attacks;
        AdditionalPowers = other.AdditionalPowers;
        Spells = other.Spells;
        Shapeshifting = other.Shapeshifting;
        NamegiverFormAbilities = other.NamegiverFormAbilities;
        AstralDetection = other.AstralDetection;
        NamegiverFormPowers = other.NamegiverFormPowers;
        Equipment = other.Equipment;
        DragonBreathNote = other.DragonBreathNote;
        HydraArmor = other.HydraArmor;
    }
}