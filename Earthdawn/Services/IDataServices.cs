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

}