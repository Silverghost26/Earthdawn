using System.Collections.Generic;
using Earthdawn.Models;
namespace EarthDawn.Services;

public interface IDataServices
{
    List<RaceDisplayCard> LoadRaces();
    List<DisciplineDisplayCard> LoadDisciplines();
    List<SpellDisplayCard> LoadSpells();
    List<TalentDisplayCard> LoadTalentsList();
    Dictionary<string, Talent> LoadTalents();

}