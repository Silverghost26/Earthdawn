using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Platform;
using Earthdawn.Models;

namespace EarthDawn.Services;

public class DataServices : IDataServices
{
    //private readonly DataLoader dataLoader = new();

    public List<RaceDisplayCard> LoadRaces()
    {
        Dictionary<string, Race> raceDictionary = DataLoader.LoadJson<Dictionary<string, Race>>(GetJson("Races.json"));
        return raceDictionary.Select(kvp => new RaceDisplayCard { Name = kvp.Key, NameGiverRace = kvp.Value }).ToList();
    }

    public List<DisciplineDisplayCard> LoadDisciplines()
    {
        Dictionary<string, Discipline> disciplineDictionary = DataLoader.LoadJson<Dictionary<string, Discipline>>(GetJson("Disciplines.json"));
        return disciplineDictionary.Select(kvp => new DisciplineDisplayCard() { Name = kvp.Key, Disciplines = kvp.Value }).ToList();
    }
    
    public List<SpellDisplayCard> LoadSpells()
    {
        Dictionary<string, SpellCircle> spellDictionary = DataLoader.LoadJson<Dictionary<string, SpellCircle>>(GetJson("spells.json"));
        return spellDictionary.Select(kvp => new SpellDisplayCard() { Name = kvp.Key, Book = kvp.Value }).ToList();
    }

    public List<TalentDisplayCard> LoadTalentsList()
    {
        Dictionary<string, Talent> talentDictionary = DataLoader.LoadJson<Dictionary<string, Talent>>(GetJson("Talents.json"));
        return talentDictionary.Select(kvp => new TalentDisplayCard() { Name = kvp.Key, Talents = kvp.Value }).ToList();
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
}