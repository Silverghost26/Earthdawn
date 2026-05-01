using Earthdawn.ViewModels;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Earthdawn.Models;
    public class SpellCircle
    {
        [JsonPropertyName("Circle_1")]
        public List<Spell> Circle_1 { get; set; }

        [JsonPropertyName("Circle_2")]
        public List<Spell> Circle_2 { get; set; }

        [JsonPropertyName("Circle_3")]
        public List<Spell> Circle_3 { get; set; }

        [JsonPropertyName("Circle_4")]
        public List<Spell> Circle_4 { get; set; }

        [JsonPropertyName("Circle_5")]
        public List<Spell> Circle_5 { get; set; }

        [JsonPropertyName("Circle_6")]
        public List<Spell> Circle_6 { get; set; }

        [JsonPropertyName("Circle_7")]
        public List<Spell> Circle_7 { get; set; }

        [JsonPropertyName("Circle_8")]
        public List<Spell> Circle_8 { get; set; }

        [JsonPropertyName("Circle_9")]
        public List<Spell> Circle_9 { get; set; }

        [JsonPropertyName("Circle_10")]
        public List<Spell> Circle_10 { get; set; }

        [JsonPropertyName("Circle_11")]
        public List<Spell> Circle_11 { get; set; }

        [JsonPropertyName("Circle_12")]
        public List<Spell> Circle_12 { get; set; }

        [JsonPropertyName("Circle_13")]
        public List<Spell> Circle_13 { get; set; }

        [JsonPropertyName("Circle_14")]
        public List<Spell> Circle_14 { get; set; }

        [JsonPropertyName("Circle_15")]
        public List<Spell> Circle_15 { get; set; }
    }

    public class Spell
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Threads")]
        public string Threads { get; set; }

        [JsonPropertyName("Casting")]
        public string Casting { get; set; }

        [JsonPropertyName("Duration")]
        public string Duration { get; set; }

        [JsonPropertyName("Effect")]
        public string Effect { get; set; }

        [JsonPropertyName("WeaveAdditional")]
        public string WeaveAdditional { get; set; }

        [JsonPropertyName("WeaveReAttune")]
        public string WeaveReAttune { get; set; }

        [JsonPropertyName("Range")]
        public string Range { get; set; }

        [JsonPropertyName("AOE")]
        public string AOE { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }
    }

public class SpellDisplayCard : ViewModelBase
{
    // private bool isSelected;
    public string Name { get; set; } = string.Empty;
    public SpellCircle Book { get; set; } = new();
    //
    // public bool IsSelected
    // {
    //     get => isSelected;
    //     set => SetProperty(ref isSelected, value);
    // }
    //
    // public string ButtonText => IsSelected ? "Remove" : "Add";

    public List<Spell> GetSpellRange(int circleRangeStart, int circleRangeEnd)
    {
        List<Spell> spellList = new List<Spell>();
        if(circleRangeStart < 1 || circleRangeStart > 15)
        {
            circleRangeStart = 1;
        }
        if (circleRangeEnd > 15  || circleRangeEnd < circleRangeStart)
        {
            circleRangeEnd = 15;
        }

        for (int i = circleRangeStart; i < circleRangeEnd; i++){
            switch (i){
                case 1:
                    spellList.AddRange(Book.Circle_1);
                    break;
                case 2:
                    spellList.AddRange(Book.Circle_2);
                    break;
                case 3:
                    spellList.AddRange(Book.Circle_3);
                    break;
                case 4:
                    spellList.AddRange(Book.Circle_4);
                    break;
                case 5:
                    spellList.AddRange(Book.Circle_5);
                    break;
                case 6:
                    spellList.AddRange(Book.Circle_6);
                    break;
                case 7:
                    spellList.AddRange(Book.Circle_7);
                    break;
                case 8:
                    spellList.AddRange(Book.Circle_8);
                    break;
                case 9:
                    spellList.AddRange(Book.Circle_9);
                    break;
                case 10:
                    spellList.AddRange(Book.Circle_10);
                    break;
                case 11:
                    spellList.AddRange(Book.Circle_11);
                    break;
                case 12:
                    spellList.AddRange(Book.Circle_12);
                    break;
                case 13:
                    spellList.AddRange(Book.Circle_13);
                    break;
                case 14:
                    spellList.AddRange(Book.Circle_14);
                    break;
                case 15:
                    spellList.AddRange(Book.Circle_15);
                    break;
            }
        }
        return spellList;
    }

    public List<Spell> GetSpellRange()
    {
        return GetSpellRange(1, 15);
    }

    public List<Spell> GetSpellCircleList(int circleRange)
    {
        return GetSpellRange(circleRange, circleRange);
    }
}

