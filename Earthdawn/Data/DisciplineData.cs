using System.Collections.Generic;

namespace Earthdawn.Data;

public class DisciplineData
{
    public string Description { get; set; } = string.Empty;
    public string ImportantAttributes { get; set; } = string.Empty;
    public string HalfMagic { get; set; } = string.Empty;
    public string Durability { get; set; } = string.Empty;
    public Dictionary<string, List<string>> TalentOptions { get; set; } = new();
    public Dictionary<string, DisciplineCircle> Circles { get; set; } = new();
}

public class DisciplineCircle
{
    public DisciplineCircle()
    {
    }
    public DisciplineCircle(DisciplineCircle disciplineCircle)
    {
        PhysicalDefense = disciplineCircle.PhysicalDefense;
        MysticalDefense = disciplineCircle.MysticalDefense;
        SocialDefense = disciplineCircle.SocialDefense;
        MysticalArmor = disciplineCircle.MysticalArmor;
        PhysicalArmor = disciplineCircle.PhysicalArmor;
        Karma = disciplineCircle.Karma;
        Initiative = disciplineCircle.Initiative;
        Recovery = disciplineCircle.Recovery;
        Special = disciplineCircle.Special;
        List<string> ft = new();
        foreach (string talent in disciplineCircle.FreeTalents)
        {
            ft.Add(talent);
        }
        FreeTalents = ft;

        List<string> t = new();
        foreach (string talent in disciplineCircle.Talents)
        {
            t.Add(talent);
        }
        Talents = t;
    }
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