
namespace Earthdawn.Models;

public class CharacterSheet
{
    //Properties
    public string CharacterName {
         get => _charactername ?? string.Empty;
         set => _charactername = value.Trim(); 
         }
    private string? _charactername;

    public string Discipline 
    {
        get => _discipline ?? string.Empty;
        set => _discipline = value.Trim();
    }
    private string? _discipline;

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

    public int CarryingCapacity
    {
        get => _carryingCapacity;
        set => _carryingCapacity = value < 0 ? 0 : value; // Ensure Carrying Capacity is non-negative
    }   
    private int _carryingCapacity;

    public int LegendPointsTotal
    {
        get => _legendpointsTotal;
        private set => _legendpointsTotal = value < 0 ? 0 : value; // Ensure Legend Points is non-negative
    }
    private int _legendpointsTotal;

    public int LegendPointsAvailable
    {
        get => _legendpointsAvailable;
        private set => _legendpointsAvailable = value < 0 ? 0 : value; // Ensure Legend Points is non-negative
    }
    private int _legendpointsAvailable;

    public string LengendaryStatus
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


}