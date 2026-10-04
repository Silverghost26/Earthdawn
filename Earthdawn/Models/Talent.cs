using System;
using Earthdawn.Data;

namespace Earthdawn.Models;

public class Talent
{
    public Talent()
    {
    }

    public Talent(Talent talent)
    {
        Rank = talent.Rank;
        Step = talent.Step;
        Name = talent.Name;
        Strain = talent.Strain;
        Action = talent.Action;
        SkillUse = talent.SkillUse;
        SkillLevel = talent.SkillLevel;
        Description = talent.Description;
        CircleObtained = talent.CircleObtained;
        
    }

    // Maps to the "Step" key
    public string Step { get; set; }

    // Maps to the "Strain" key (Since it's an integer, we use 'int')
    public int Strain { get; set; }

    // Maps to the "Action" key
    public string Action { get; set; }

    // Maps to the "SkillUse" key
    public string SkillUse { get; set; }

    // Maps to the "SkillLevel" key
    public string SkillLevel { get; set; }

    // Maps to the "Description" key
    public string Description { get; set; }
    
    public int Rank { get; set; }
    public int CircleObtained { get; set; }
    public string Name { get; set; }

    public AttributesTypes GetStepAttributeEnum()
    {
        if(Step.Contains("PER", StringComparison.OrdinalIgnoreCase))
            return AttributesTypes.Per;
        if(Step.Contains("STR", StringComparison.OrdinalIgnoreCase))
            return AttributesTypes.Str;
        if(Step.Contains("DEX", StringComparison.OrdinalIgnoreCase))
            return AttributesTypes.Dex;
        if(Step.Contains("TOU", StringComparison.OrdinalIgnoreCase))
            return AttributesTypes.Tou;
        if(Step.Contains("WIL", StringComparison.OrdinalIgnoreCase))
            return AttributesTypes.Wil;
        if(Step.Contains("CHR", StringComparison.OrdinalIgnoreCase) || Step.Contains("CHA", StringComparison.OrdinalIgnoreCase))
            return AttributesTypes.Chr;
        return AttributesTypes.None;
    }
}