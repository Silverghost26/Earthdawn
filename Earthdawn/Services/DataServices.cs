using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Platform;
using Earthdawn.Models;
using Earthdawn.Data;
using EarthDawn.Models;

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

    public List<WeaponDisplayCard> LoadWeaponsList()
    {
        Dictionary<string, Weapon> weaponDictionary = DataLoader.LoadJson<Dictionary<string, Weapon>>(GetJson("Weapons.json"));
        return weaponDictionary.Select(kvp => new WeaponDisplayCard() { Name = kvp.Key, Weapons = kvp.Value }).ToList();
    }

    public List<ArmorDisplayCard> LoadArmorList()
    {
        Dictionary<string, Armor> armorDictionary = DataLoader.LoadJson<Dictionary<string, Armor>>(GetJson("Armor.json"));
        return armorDictionary.Select(kvp => new ArmorDisplayCard() { Name = kvp.Key, Armors = kvp.Value }).ToList();
    }

    public List<ShieldDisplayCard> LoadShieldsList()
    {
        Dictionary<string, Shield> shieldDictionary = DataLoader.LoadJson<Dictionary<string, Shield>>(GetJson("shields.json"));
        return shieldDictionary.Select(kvp => new ShieldDisplayCard() { Name = kvp.Key, Shields = kvp.Value }).ToList();
    }

    public List<EquipmentDisplayCard> LoadEquipmentList()
    {
        Dictionary<string, Equipment> equipmentDictionary = DataLoader.LoadJson<Dictionary<string, Equipment>>(GetJson("Equipment.json"));
        foreach (var key in equipmentDictionary.Keys)
        {
            equipmentDictionary[key].Name = key;
        }
        return equipmentDictionary.Select(kvp => new EquipmentDisplayCard() { Name = kvp.Key, Equipment = kvp.Value }).ToList();
        
    }

    public List<MountDisplayCard> LoadMountsList()
    {
        Dictionary<string, Mount> mountDictionary = DataLoader.LoadJson<Dictionary<string, Mount>>(GetJson("Mounts.json"));
        return mountDictionary.Select(kvp => new MountDisplayCard() { Name = kvp.Key, Mounts = kvp.Value }).ToList();
    }

    public Dictionary<string, Talent> LoadTalents()
    {
        return DataLoader.LoadJson<Dictionary<string, Talent>>(GetJson("Talents.json"));
    }
    
    // New loaders for equipment tables
    public List<TradeService> LoadTradeServices()
    {
        return DataLoader.LoadJson<List<TradeService>>(GetJson("TradeServiceTable.json"));
    }

    public List<Accommodation> LoadAccommodations()
    {
        return DataLoader.LoadJson<List<Accommodation>>(GetJson("AccommodationTable.json"));
    }

    public List<Clothing> LoadClothingItems()
    {
        return DataLoader.LoadJson<List<Clothing>>(GetJson("ClothingTable.json"));
    }

    public List<Provender> LoadProvenderItems()
    {
        return DataLoader.LoadJson<List<Provender>>(GetJson("ProvenderTable.json"));
    }
    
    public List<Creature> LoadCreatures()
    {
        return DataLoader.LoadJson<List<Creature>>(GetJson("Creatures.json"));
    }
    
    // New loader for Dragon Types
    public List<DragonType> LoadDragonTypes()
    {
        DragonTypesContainer container = DataLoader.LoadJson<DragonTypesContainer>(GetJson("DragonTypes.json"));
        return container.DragonTypes ?? new List<DragonType>();
    }
    
    // New loader for Diseases
    public List<Disease> LoadDiseases()
    {
        DiseasesContainer container = DataLoader.LoadJson<DiseasesContainer>(GetJson("Diseases.json"));
        return new List<Disease>(container.Values);
    }
    
    // New loader for Poisons
    public List<Poison> LoadPoisons()
    {
        PoisonsContainer container = DataLoader.LoadJson<PoisonsContainer>(GetJson("Poisons.json"));
        return new List<Poison>(container.Values);
    }
    
    // New loader for Traps
    public List<Trap> LoadTraps()
    {
        TrapsContainer container = DataLoader.LoadJson<TrapsContainer>(GetJson("Traps.json"));
        return new List<Trap>(container.Values);
    }
    
    // New loader for Spirit Powers
    public List<SpiritPower> LoadSpiritPowers()
    {
        return DataLoader.LoadJson<List<SpiritPower>>(GetJson("SpiritPower.json"));
    }
    
    public List<DragonPower> LoadDragonPowers()
    {
        return DataLoader.LoadJson<List<DragonPower>>(GetJson("DragonPowers.json"));
    }
    
    public List<HorrorPower> LoadHorrorPowers()
    {
        return DataLoader.LoadJson<List<HorrorPower>>(GetJson("HorrorPowers.json"));
    }
    
    
    
    // New loader for Legend Award Table
    public List<LegendAwardEntry> LoadLegendAwardTable()
    {
        LegendAwardTable container = DataLoader.LoadJson<LegendAwardTable>(GetJson("LegendAwardTable.json"));
        return new List<LegendAwardEntry>(container.Values);
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