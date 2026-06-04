using System.Collections.Generic;
using Earthdawn.Models;

namespace Earthdawn.Data;
    public class SpellCircle
    {
        public List<Spell> Circle_1 { get; set; }
        public List<Spell> Circle_2 { get; set; }
        public List<Spell> Circle_3 { get; set; }
        public List<Spell> Circle_4 { get; set; }
        public List<Spell> Circle_5 { get; set; }
        public List<Spell> Circle_6 { get; set; }
        public List<Spell> Circle_7 { get; set; }
        public List<Spell> Circle_8 { get; set; }
        public List<Spell> Circle_9 { get; set; }
        public List<Spell> Circle_10 { get; set; }
        public List<Spell> Circle_11 { get; set; }
        public List<Spell> Circle_12 { get; set; }
        public List<Spell> Circle_13 { get; set; }
        public List<Spell> Circle_14 { get; set; }
        public List<Spell> Circle_15 { get; set; }
    }



public class SpellDisplayCard
{
    public SpellDisplayCard()
    {
    }

    public string Name { get; set; } = string.Empty;
    public SpellCircle Book { get; set; } = new();

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

    public List<Spell> GetyAllSpells()
    {
        return GetSpellRange(1, 15);
    }

    public List<Spell> GetSpellCircleList(int circleRange)
    {
        return GetSpellRange(circleRange, circleRange);
    }
}

