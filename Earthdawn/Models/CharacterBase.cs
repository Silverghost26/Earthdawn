using System.Collections.Generic;
using System.Linq;
using System.Text;
using Earthdawn.Data;
using EarthDawn.Models;

namespace Earthdawn.Models;
public class CharacterBase
{
    //**********************************************Private Members********************************************
    protected List<Discipline> _disciplines;
    protected List<Skill> _skills;
    private List<Weapon> _weapons;
    private List<Armor> _armor;
    private List<Shield> _shields;
    private List<Equipment> _equipment;
    private List<Mount> _mounts;
    private List<Clothing> _clothing;
    //**********************************************Constructors***********************************
    public CharacterBase()
    {
        _charAttributes = new Attributes();
        _disciplines = new ();
        _weapons = new List<Weapon>();
        _armor = new List<Armor>();
        _shields = new List<Shield>();
        _equipment = new List<Equipment>();
        _mounts = new List<Mount>();
        _clothing = new List<Clothing>();
        _racialAbilities  = new List<SpecialAbility>();
    }
    
    //***********************************Private Vars*************************************************
    protected Attributes? _charAttributes;
    protected void SetCharAttributes(Attributes? value)
    {
        _charAttributes = value;
    }
        
    //*****************************************Properties**********************************************
    public string CharacterName { get; set; }
    public string Race { get; set; }
    public List<SpecialAbility> RacialAbilities
    {
        get => _racialAbilities;
        set => _racialAbilities = value ?? new List<SpecialAbility>();
    }
    private List<SpecialAbility> _racialAbilities;
    public string Sex { get; set; }
    public int Age { get; set; }
    public string Height { get; set; }
    public string Weight { get; set; }
    public string HairColor { get; set; }
    public string EyeColor { get; set; }
    public string SkinColor { get; set; }
    public int MovementRate { get; set; }
    public int FlyingMovementRate { get; set; }
    public int CarryingCapacity { get; set; }

    public Money Money { get; set; }
    public List<Weapon> Weapons { get {return _weapons;} }
    public List<Armor> Armor { get {return _armor;} }
    public List<Shield> Shields { get {return _shields;} }
    public List<Equipment> Equipment { get {return _equipment;} }
    public List<Mount> Mounts { get {return _mounts;} }
    public List<Clothing> Clothing { get {return _clothing;} }
    public List<Skill> Skills { get {return _skills;} }
    
    public Attributes CharacterAttributes
    {
        get { return _charAttributes; }
    }

    public List<Skill> CharacterSkills
    {
        get { return _skills; }
    }
    
    public int Initiative {
        get
        {
            return _charAttributes.GetStepNumber(AttributesTypes.Dex);
        }
        set; 
    }
    
    public int PhysicalDefense
    {
        get
        {
            return _charAttributes.GetBasePhysicalDefense();
        }
        set;
    }
    
    
    public int MysticDefense
    {
        get
        {
            return _charAttributes.GetBaseMysticDefense();
        }
        set;
    }
    
    public int PhysicalArmor
    {
        get
        {
            return 0;
        }
        set;
    }
    
    public int MysticalArmor
    {
        get
        {
            return _charAttributes.GetMysticArmor();
        }
    }
    
    public int SocialDefense
    {
        get
        {
            return _charAttributes.GetBaseSocialDefense();
        }
        set;
    }
    
    public int UnconsciousRating
    {
        get => _charAttributes.GetUnconsciousnessRating();
    }
    
    public int DeathRating
    {
        get => _charAttributes.GetDeathRating();
    }
    
    public int RecoveryTests
    {
        get => _charAttributes.GetRecoveryTests();
    }
    
    public int WoundThreshold
    {
        get => _charAttributes.GetWoundThreshold();
    }

    public int Karma { get; set; }
    public int KarmaModifier { get; set; }
    public int MaxKarma { get; set; }
    
    //***************************************************************Functions***********************************************
    public void AddDiscipline(Discipline discipline)
    {
        if (_disciplines.Contains(discipline) || _disciplines.Count >= 4)
            return;
        _disciplines.Add(discipline);
    }
    public int GetNumberOfDisciplines()
    {
        return _disciplines.Count;
    }
    
    public ref readonly List<Discipline> GetDiscipline()
    {
        return ref _disciplines;
    }

    public void AddNewOptionTalent(Talent talent, string discipline)
    {
        foreach (Discipline ds in _disciplines)
        {
            if (ds.DisciplineName == discipline)
            {
                ds.AddNewOptionalTalent(talent);
            }
        }
    }

    public void AddWeapon(Weapon weapon)
    {
        if (weapon != null)
        {
            _weapons.Add(weapon);
        }
    }

    public void RemoveWeapon(Weapon weapon)
    {
        if (_weapons.Contains(weapon))
        {
            _weapons.Remove(weapon);
        }
    }
    
    public void AddArmor(Armor armor)
    {
        if (armor != null)
        {
            _armor.Add(armor);
        }
    }

    public void RemoveArmor(Armor armor)
    {
        if (_armor.Contains(armor))
        {
            _armor.Remove(armor);
        }
    }
    
    public void AddShield(Shield shield)
    {
        if (shield != null)
        {
            _shields.Add(shield);
        }
    }

    public void RemoveShield(Shield shield)
    {
        if (_shields.Contains(shield))
        {
            _shields.Remove(shield);
        }
    }
    
    public void AddEquipment(Equipment equipment)
    {
        if (equipment != null)
        {
            _equipment.Add(equipment);
        }
    }

    public void RemoveEquipment(Equipment equipment)
    {
        if (_equipment.Contains(equipment))
        {
            _equipment.Remove(equipment);
        }
    }
    public void AddMount(Mount mount)
    {
        if (mount != null)
        {
            _mounts.Add(mount);
        }
    }

    public void RemoveMount(Mount mount)
    {
        if (_mounts.Contains(mount))
        {
            _mounts.Remove(mount);
        }
    }
    
    public void AddClothing(Clothing clothing)
    {
        if (clothing != null)
        {
            _clothing.Add(clothing);
        }
    }

    public void RemoveClothing(Clothing clothing)
    {
        if (_clothing.Contains(clothing))
        {
            _clothing.Remove(clothing);
        }
    }

    public bool AddSkill(Skill skill)
    {
        if (skill == null)
        {
            return false;
        }

        if (_skills.Any(s => s.Name == skill.Name))
        {
            return false;
        }
        _skills.Add(skill);
        return true;
    }

    public bool RemoveSkill(Skill skill)
    {
        if (skill == null)
            return false;
 
        if (_skills.Any(s => s.Name == skill.Name))
        {
            _skills.RemoveAll(obj => obj.Name == skill.Name);
            return true;
        }
        return false;
    }
    
}