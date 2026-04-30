using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Platform;
using Earthdawn.Models;

namespace EarthDawn.Services;

public class DataServices : IDataServices
{
    private readonly DataLoader dataLoader = new();

    public List<RaceDisplayCard> LoadRaces()
    {
        Dictionary<string, Race> raceDictionary = dataLoader.LoadJson<Dictionary<string, Race>>(getJson("Races.json"));
        return raceDictionary.Select(kvp => new RaceDisplayCard { Name = kvp.Key, NameGiverRace = kvp.Value }).ToList();
    }

    public List<DisciplineDisplayCard> LoadDisciplines()
    {
        Dictionary<string, Discipline> disciplineDictionary = dataLoader.LoadJson<Dictionary<string, Discipline>>(getJson("Disciplines.json"));
        return disciplineDictionary.Select(kvp => new DisciplineDisplayCard() { Name = kvp.Key, Disciplines = kvp.Value }).ToList();
    }
    
    public List<SpellDisplayCard> LoadSpells()
    {
        Dictionary<string, SpellCircle> spellDictionary = dataLoader.LoadJson<Dictionary<string, SpellCircle>>(getJson("Spells.json"));
        return spellDictionary.Select(kvp => new SpellDisplayCard() { Name = kvp.Key, Book = kvp.Value }).ToList();
    }

    private string getJson(string file)
    {
        string path = "avares://Earthdawn/Assets/Data/" + file;
        var uri = new Uri(path);
        using var stream = AssetLoader.Open(uri);
        using var reader = new StreamReader(stream);

        string jsonText = reader.ReadToEnd();
        return jsonText;
    }
}