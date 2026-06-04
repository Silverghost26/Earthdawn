using System.Collections.Generic;
using Earthdawn.Data;
using EarthDawn.Services;

namespace Earthdawn.Models;
    public class CharacterDiscipline
    {
        //**************************************Private Var*******************************************
        private Dictionary<CastingClasses, List<Spell>> _spells = new Dictionary<CastingClasses, List<Spell>>();
        
        //**************************************Constructors*******************************************
        public CharacterDiscipline(string disciplineName, DisciplineCircle disciplineCircle, int durability, int circlePrecedent, Attributes att)
        {
            DisciplineName = disciplineName;
            Durability = durability;
            CirclePrecedence = circlePrecedent;
            AddNewDisciplineCircle(disciplineCircle, att, 1);
        }
        
        //**************************************Properties*******************************************
        public string? DisciplineName { get; init; } = string.Empty;
        public int CirclePrecedence { get; init; }
        public int CircleLevel { get; set; }
        public int Durability { get; init; }
        public int PhysicalDefenseBonus { get; set; }
        public int MysticalDefenseBonus { get; set; }
        public int SocialDefenseBonus { get; set; }
        public int PhysicalArmorBonus { get; set; }
        public int MysticalArmorBonus { get; set; }
        public int RecoveryTestBonus { get; set; }
        public int InitiativeBonus { get; set; }

        public List<CharacterTalent> Talents
        {
            get => _talents;
        }
        private List<CharacterTalent> _talents = new List<CharacterTalent>();

        public List<string>? CircleSpecials
        {
            get => _circleSpecials;
        }
        private List<string>? _circleSpecials = new List<string>();

        public List<string>? KarmaSpecials
        {
            get => _karmaSpecials;
        }
        private List<string> _karmaSpecials = new List<string>();
        
        //*****************************************functions*******************************************************
        public List<CharacterTalent> GetCharacterTalents()
        {
            return Talents;
        }
        
        public void AddNewCharacterTalent(string talentName, Talent tnt, bool isFree, bool isOptional, DisciplineTiers dt, Attributes at, int startingTalentLevel = 0)
        {
            CharacterTalent newTalent = new CharacterTalent(talentName, tnt, dt, isFree, isOptional);
            newTalent.UpdateRank(startingTalentLevel, at);
            _talents.Add(newTalent);
        }

        public void UpdateCharacterTalent(string talentName, int value, Attributes att)
        {
            //Find the existing talent
            foreach (CharacterTalent ct in _talents)
            {
                if (ct.TalentName == talentName)
                {
                    ct.UpdateRank(value, att);
                }
            }
        }

        public void AddNewDisciplineCircle(DisciplineCircle c, Attributes at, int circleLevel)
        {
            var ds = new DataServices();
            var talents = ds.LoadTalents();
            PhysicalDefenseBonus += c.PhysicalDefense;
            MysticalDefenseBonus += c.MysticalDefense;
            SocialDefenseBonus += c.SocialDefense;
            PhysicalArmorBonus += c.PhysicalArmor;
            MysticalArmorBonus += c.MysticalArmor;
            RecoveryTestBonus += c.Recovery;
            InitiativeBonus += c.Initiative;
            CircleLevel = circleLevel;

            if (!string.IsNullOrEmpty(c.Special))
            {
                _circleSpecials.Add(c.Special);
            }

            if (!string.IsNullOrEmpty(c.Karma))
            {
                _karmaSpecials.Add((c.Karma));
            }
            
            DisciplineTiers dt;
            if (CircleLevel < 5)
                dt = DisciplineTiers.Novice;
            if (CircleLevel < 9)
                dt = DisciplineTiers.Journeyman;
            if (CircleLevel < 13)
                dt = DisciplineTiers.Warden;
            else
            {
                dt = DisciplineTiers.Master;
            }

            //Add All the Discipline Talents.
            foreach (string talent in c.Talents)
            {
                string tempTalent = string.Empty;
                if (talent.Contains("Thread Weaving"))
                {
                    tempTalent = "Thread Weaving";
                }
                else
                {
                    tempTalent = talent;
                }
                var newTalent = talents[tempTalent];
                AddNewCharacterTalent(talent, newTalent, false, false, dt, at);
            }
            //Add All Free Talents
            foreach (string talent in c.FreeTalents)
            {
                var newTalent = talents[talent];
                AddNewCharacterTalent(talent, newTalent, true, false, dt, at, CircleLevel);
            }
        }
        public List<Spell> GetSpellsOfDiscipline(CastingClasses castingClass)
        {
            if (_spells.ContainsKey(castingClass))
            {
                return _spells[castingClass];
            }
            else
            {
                return new List<Spell>();
            }
        }

        public Dictionary<CastingClasses, List<Spell>> GetAllSpells()
        {
            return _spells;
        }

        public void AddNewSpell(Spell spell, CastingClasses castingClass)
        {
            switch (castingClass)
            {
                case CastingClasses.Elementalist:
                    if ("Elementalist" == DisciplineName)
                        AddSpellToSpellBook(spell, castingClass);
                    break;
                case CastingClasses.Illusionist:
                    if ("Illusionist" == DisciplineName)
                        AddSpellToSpellBook(spell, castingClass);
                    break;
                case CastingClasses.Nethermancer:
                    if ("Nethermancer" == DisciplineName)
                        AddSpellToSpellBook(spell, castingClass);
                    break;
                case CastingClasses.Shaman:
                    if ("Shaman" == DisciplineName)
                        AddSpellToSpellBook(spell, castingClass);
                    break;
                case CastingClasses.Wizard:
                    if ("Wizard" == DisciplineName)
                        AddSpellToSpellBook(spell, castingClass);
                    break;
                default:
                    break;
            }
        }

        private void AddSpellToSpellBook(Spell spell, CastingClasses castingClass)
        {
            if(_spells[castingClass] is null)
                _spells.Add(castingClass, new List<Spell>());
            if(!_spells[castingClass].Contains(spell))
                _spells[castingClass].Add(spell);
        }
    }