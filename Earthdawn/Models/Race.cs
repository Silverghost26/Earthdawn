using System.Collections.Generic;

namespace Earthdawn.Models;

public class Race
{
    public Race()
    {
    }

    public Race(Race race)
    {
        Description = race.Description;
        DEX = race.DEX;
        STR = race.STR;
        TOU = race.TOU;
        PER = race.PER;
        WIL = race.WIL;
        CHA = race.CHA;
        Movement = race.Movement;
        FlyingMovement = race.FlyingMovement;
        KarmaMod = race.KarmaMod;
        
        List<SpecialAbility> saList = new();
        foreach (SpecialAbility ability in race.Abilities)
        {
           saList.Add(new SpecialAbility(ability));
        }
        Abilities = saList;
    }
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

public class SpecialAbility
{
    public SpecialAbility()
    {
    }
    public SpecialAbility(SpecialAbility ability)
    {
        Ability = ability.Ability;
        Description = ability.Description;
    }
    
    public string Ability { get; set; }
    public string Description { get; set; }
}