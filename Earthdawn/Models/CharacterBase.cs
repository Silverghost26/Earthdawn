using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using CommunityToolkit.Mvvm.ComponentModel;
using Earthdawn.Data;
using EarthDawn.Services;

namespace Earthdawn.Models;

public class CharacterBase : ObservableObject
{
    public CharacterBase()
    {
        _charAttributes = new Attributes();
        _startingAttributes = new Attributes();
        _characterDisciplineCircles = new List<DisciplineCircle>();
        SubscribeToAttributeChanges();
    }
    
    //***********************************Private Vars*************************************************
    protected Attributes? _charAttributes;
    protected Attributes? _startingAttributes;
    private List<DisciplineCircle> _characterDisciplineCircles;
    
    private void SubscribeToAttributeChanges()
    {
        if (_charAttributes != null)
        {
            _charAttributes.PropertyChanged += OnCharAttributesPropertyChanged;
        }
        if (_startingAttributes != null)
        {
            _startingAttributes.PropertyChanged += OnStartingAttributesPropertyChanged;
        }
    }
    protected void SetCharAttributes(Attributes? value)
    {
        if (_charAttributes != null)
        {
            _charAttributes.PropertyChanged -= OnCharAttributesPropertyChanged;
        }
        _charAttributes = value;
        if (_charAttributes != null)
        {
            _charAttributes.PropertyChanged += OnCharAttributesPropertyChanged;
        }
        RaiseAllAttributePropertyChanges();
    }

    protected void SetStartingAttributes(Attributes? value)
    {
        if (_startingAttributes != null)
        {
            _startingAttributes.PropertyChanged -= OnStartingAttributesPropertyChanged;
        }
        _startingAttributes = value;
        if (_startingAttributes != null)
        {
            _startingAttributes.PropertyChanged += OnStartingAttributesPropertyChanged;
        }
        RaiseAllOriginalAttributePropertyChanges();
    }

    private void OnCharAttributesPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // When an attribute changes, notify all dependent properties
        switch (e.PropertyName)
        {
            case nameof(Attributes.Dexterity):
                OnPropertyChanged(nameof(Dexterity));
                OnPropertyChanged(nameof(Initiative));
                OnPropertyChanged(nameof(PhysicalDefense));
                break;
            case nameof(Attributes.Strength):
                OnPropertyChanged(nameof(Strength));
                break;
            case nameof(Attributes.Toughness):
                OnPropertyChanged(nameof(Toughness));
                OnPropertyChanged(nameof(UnconsciousRating));
                OnPropertyChanged(nameof(DeathRating));
                OnPropertyChanged(nameof(WoundThreshold));
                OnPropertyChanged(nameof(RecoveryTests));
                break;
            case nameof(Attributes.Perception):
                OnPropertyChanged(nameof(Perception));
                OnPropertyChanged(nameof(MysticDefense));
                break;
            case nameof(Attributes.Willpower):
                OnPropertyChanged(nameof(Willpower));
                OnPropertyChanged(nameof(MysticalArmor));
                break;
            case nameof(Attributes.Charisma):
                OnPropertyChanged(nameof(Charisma));
                OnPropertyChanged(nameof(SocialDefense));
                break;
        }
        OnPropertyChanged(nameof(Karma));
    }

    private void OnStartingAttributesPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(Attributes.Dexterity):
                OnPropertyChanged(nameof(OriginalDex));
                break;
            case nameof(Attributes.Strength):
                OnPropertyChanged(nameof(OriginalStr));
                break;
            case nameof(Attributes.Toughness):
                OnPropertyChanged(nameof(OriginalTou));
                break;
            case nameof(Attributes.Perception):
                OnPropertyChanged(nameof(OriginalPer));
                break;
            case nameof(Attributes.Willpower):
                OnPropertyChanged(nameof(OriginalWil));
                break;
            case nameof(Attributes.Charisma):
                OnPropertyChanged(nameof(OriginalChr));
                break;
        }
    }

    private void RaiseAllAttributePropertyChanges()
    {
        OnPropertyChanged(nameof(Dexterity));
        OnPropertyChanged(nameof(Strength));
        OnPropertyChanged(nameof(Toughness));
        OnPropertyChanged(nameof(Perception));
        OnPropertyChanged(nameof(Willpower));
        OnPropertyChanged(nameof(Charisma));
        OnPropertyChanged(nameof(Initiative));
        OnPropertyChanged(nameof(PhysicalDefense));
        OnPropertyChanged(nameof(MysticDefense));
        OnPropertyChanged(nameof(SocialDefense));
        OnPropertyChanged(nameof(UnconsciousRating));
        OnPropertyChanged(nameof(DeathRating));
        OnPropertyChanged(nameof(WoundThreshold));
        OnPropertyChanged(nameof(RecoveryTests));
        OnPropertyChanged(nameof(MysticalArmor));
        OnPropertyChanged(nameof(Karma));
    }

    private void RaiseAllOriginalAttributePropertyChanges()
    {
        OnPropertyChanged(nameof(OriginalDex));
        OnPropertyChanged(nameof(OriginalStr));
        OnPropertyChanged(nameof(OriginalTou));
        OnPropertyChanged(nameof(OriginalPer));
        OnPropertyChanged(nameof(OriginalWil));
        OnPropertyChanged(nameof(OriginalChr));
    }
    //************************************Properties**************************************************

        
    public string CharacterName { get; set; }
    public string Race { get; set; }
    public List<SpecialAbility> RacialAbilities
    {
        get => _racialAbilities ??= new List<SpecialAbility>();
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

 public int Dexterity
    {
        get
        {
            return _charAttributes.Dexterity;
        }
        set
        {
            _charAttributes.Dexterity = value;
        }
    }
    public int Strength
    {
        get
        {
            return _charAttributes.Strength;
        }
        set
        {
            _charAttributes.Strength = value;
        }
    }
    public int Toughness {
        get
        {
            return _charAttributes.Toughness;
        }
        set
        {
            _charAttributes.Toughness = value;
        }
    }

    public int Perception
    {
        get
        {
            return _charAttributes.Perception;
        }
        set
        {
            _charAttributes.Perception = value;
        }
    }

    public int Willpower
    {
        get
        {
            return _charAttributes.Willpower;
        }
        set
        {
            _charAttributes.Willpower = value;
        }
    }

    public int Charisma
    {
        get
        {
            return _charAttributes.Charisma;
        }
        set
        {
            _charAttributes.Charisma = value;
        }
    }

    public int OriginalChr
    {
        get
        {
            return _startingAttributes.Charisma;
        }
        set
        {
            _startingAttributes.Charisma = value;
        }
    }

    public int OriginalDex
    {
        get
        {
            return _startingAttributes.Dexterity;
        }
        set
        {
            _startingAttributes.Dexterity = value;
        }
    }

    public int OriginalPer
    {
        get
        {
            return _startingAttributes.Perception;
        }
        set
        {
            _startingAttributes.Perception = value;
        }
    }

    public int OriginalStr
    {
        get
        {
            return _startingAttributes.Strength;
        }
        set
        {
            _startingAttributes.Strength = value;
        }
    }

    public int OriginalTou
    {
        get
        {
            return _startingAttributes.Toughness;
        }
        set
        {
            _startingAttributes.Toughness = value;
        }
    }

    public int OriginalWil
    {
        get
        {
            return _startingAttributes.Willpower;
        }
        set
        {
            _startingAttributes.Willpower = value;
        }
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

    
    public void AddNewDiscipline(DisciplineDisplayCard ddc)
    {
        foreach (var cd in _characterDisciplineCircles)
        {
            if (cd.DisciplineName == ddc.Name)
                return;
        }
        int currentCircleTotal = _characterDisciplineCircles.Count;
        int.TryParse(ddc.Disciplines.Durability, out int durability);
        _characterDisciplineCircles.Add(new DisciplineCircle(ddc.Name, ddc.Disciplines.Circles["First"], durability, currentCircleTotal + 1, _charAttributes));
    }

    public void AddNewCircleToExistingDiscipline(string disciplineName, Circle circle, Attributes att, int circleLevel)
    {
        foreach (var dc in _characterDisciplineCircles)
        {
            if (dc.DisciplineName == disciplineName)
            {
                if (dc.CircleLevel + 1 == circleLevel)
                {
                    dc.AddNewDisciplineCircle(circle, att, circleLevel);
                }
                else
                {
                    throw new Exception(
                        "Circle Level: " + circleLevel+ "is not the expected circle of: " + 
                                        dc.CircleLevel + "for Discipline: " + disciplineName);
                }
            }
            else
            {
                throw new Exception(
                    disciplineName + " is not a characters current discipline.  Use Add new Discipline.");
            }
        }
    }

    public DisciplineCircle GetDisciplineCircles(string discipline)
    {
        foreach (DisciplineCircle dc in _characterDisciplineCircles)
        {
            if (dc.DisciplineName == discipline)
                return dc;
        }

        return null;
    }

    public List<string> GetAllCharacterDisciplines()
    {
        List<string> allDisciplineNames = new();
        foreach (var discipline in _characterDisciplineCircles)
        {
            allDisciplineNames.Add(discipline.DisciplineName);
        }

        return allDisciplineNames;
    }

    
    //************************************************************Internal Classes******************************************
    public class DisciplineCircle
    {
        public DisciplineCircle(string disciplineName, Circle circle, int durability, int circlePrecedent, Attributes att)
        {
            DisciplineName = disciplineName;
            Durability = durability;
            CirclePrecedence = circlePrecedent;
            AddNewDisciplineCircle(circle, att, 1);
        }
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

        public void AddNewDisciplineCircle(Circle c, Attributes at, int circleLevel)
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
    }
}