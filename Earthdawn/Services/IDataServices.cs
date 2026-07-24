using System.Collections.Generic;
using Earthdawn.Models;
using Earthdawn.Data;
namespace EarthDawn.Services;

public interface IDataServices
{
    List<RaceDisplayCard> LoadRaces();
    List<DisciplineDisplayCard> LoadDisciplines();
    List<SpellDisplayCard> LoadSpells();
    List<TalentDisplayCard> LoadTalentsList();
    List<SkillDisplayCard> LoadSkillsList();
    List<WeaponDisplayCard> LoadWeaponsList();
    List<ArmorDisplayCard> LoadArmorList();
    List<ShieldDisplayCard> LoadShieldsList();
    List<EquipmentDisplayCard> LoadEquipmentList();
    List<MountDisplayCard> LoadMountsList();
    Dictionary<string, Talent> LoadTalents();
    List<TradeService> LoadTradeServices();
    List<Accommodation> LoadAccommodations();
    List<Clothing> LoadClothingItems();
    List<Provender> LoadProvenderItems();
    List<Creature> LoadCreatures();
    List<DragonType> LoadDragonTypes();
    List<Disease> LoadDiseases();
    List<Poison> LoadPoisons();
    List<Trap> LoadTraps();
    List<SpiritPower> LoadSpiritPowers();
    List<LegendAwardEntry> LoadLegendAwardTable();
    List<DragonPower> LoadDragonPowers(); // Added DragonPowers loader
    List<HorrorPower> LoadHorrorPowers(); // Added HorrorPowers loader
    
    // New loader for Horror Constructs
    List<HorrorConstruct> LoadHorrorConstructs();
    
    // New loader for Dragon-like Creatures
    List<DragonLikeCreature> LoadDragonLikeCreatures();
    
    // New loader for Horrors
    List<Horror> LoadHorrors();
}