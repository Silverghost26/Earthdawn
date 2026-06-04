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
    Dictionary<string, Talent> LoadTalents();

}