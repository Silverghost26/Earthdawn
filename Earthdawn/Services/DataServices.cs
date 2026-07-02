using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Platform;
using Earthdawn.Models;
using Earthdawn.Data;

namespace EarthDawn.Services;

public class DataServices : IDataServices
{
    public List<RaceDisplayCard> LoadRaces()
    {
        Dictionary<string, Race> raceDictionary = DataLoader.LoadJson<Dictionary<string, Race>>(GetJson("Races.json"));
        return raceDictionary.Select(kvp => new RaceDisplayCard { Name = kvp.Key, NameGiverRace = kvp.Value }).ToList();
    }

    public List<DisciplineDisplayCard> LoadDisciplines()
    {
        Dictionary<string, DisciplineData> disciplineDictionary = DataLoader.LoadJson<Dictionary<string, DisciplineData>>(GetJson("Disciplines.json"));
        return disciplineDictionary.Select(kvp => new DisciplineDisplayCard() { Name = kvp.Key, Disciplines = kvp.Value }).ToList();
    }
    
    public List<SpellDisplayCard> LoadSpells()
    {
        Dictionary<string, SpellCircle> spellDictionary = DataLoader.LoadJson<Dictionary<string, SpellCircle>>(GetJson("spells.json"));
        List<SpellDisplayCard> spellDisplayCards = spellDictionary.Select(kvp => new SpellDisplayCard() { Name = kvp.Key, Book = kvp.Value }).ToList();
        foreach (SpellDisplayCard sdc in spellDisplayCards)
        {
            UpdateSpellsWithCircle(sdc.Book);
        }
        return spellDisplayCards;
    }

    public List<TalentDisplayCard> LoadTalentsList()
    {
        Dictionary<string, Talent> talentDictionary = DataLoader.LoadJson<Dictionary<string, Talent>>(GetJson("Talents.json"));
        return talentDictionary.Select(kvp => new TalentDisplayCard() { Name = kvp.Key, Talents = kvp.Value }).ToList();
    }

    public List<SkillDisplayCard> LoadSkillsList()
    {
        Dictionary<string, Skill> skillDictionary = DataLoader.LoadJson<Dictionary<string, Skill>>(GetJson("skills.json"));
        return skillDictionary.Select(kvp => new SkillDisplayCard() { Name = kvp.Key, Skills = kvp.Value }).ToList();
    }

    public Dictionary<string, Talent> LoadTalents()
    {
        return DataLoader.LoadJson<Dictionary<string, Talent>>(GetJson("Talents.json"));
    }
    

    private string GetJson(string file)
    {
        string path = "avares://Earthdawn/Assets/Data/" + file;
        var uri = new Uri(path);
        using var stream = AssetLoader.Open(uri);
        using var reader = new StreamReader(stream);

        string jsonText = reader.ReadToEnd();
        return jsonText;
    }

    private void UpdateSpellsWithCircle(SpellCircle sc)
    {
        foreach (Spell spell in sc.Circle_1)
        {
            spell.Circle = 1;
        }
        foreach (Spell spell in sc.Circle_2)
        {
            spell.Circle = 2;
        }
        foreach (Spell spell in sc.Circle_3)
        {
            spell.Circle = 3;
        }
        foreach (Spell spell in sc.Circle_4)
        {
            spell.Circle = 4;
        }
        foreach (Spell spell in sc.Circle_5)
        {
            spell.Circle = 5;
        }
        foreach (Spell spell in sc.Circle_6)
        {
            spell.Circle = 6;
        }
        foreach (Spell spell in sc.Circle_7)
        {
            spell.Circle = 7;
        }
        foreach (Spell spell in sc.Circle_8)
        {
            spell.Circle = 8;
        }
        foreach (Spell spell in sc.Circle_9)
        {
            spell.Circle = 9;
        }
        foreach (Spell spell in sc.Circle_10)
        {
            spell.Circle = 10;
        }
        foreach (Spell spell in sc.Circle_11)
        {
            spell.Circle = 11;
        }
        foreach (Spell spell in sc.Circle_12)
        {
            spell.Circle = 12;
        }
        foreach (Spell spell in sc.Circle_13)
        {
            spell.Circle = 13;
        }
        foreach (Spell spell in sc.Circle_14)
        {
            spell.Circle = 14;
        }
        foreach (Spell spell in sc.Circle_15)
        {
            spell.Circle = 15;
        }
    }
}