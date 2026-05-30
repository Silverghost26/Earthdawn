namespace Earthdawn.Models;

public class Spell
{
        public Spell()
        {
        }

        public Spell(Spell spell)
        {
                Name = spell.Name;
                Threads = spell.Threads;
                Casting = spell.Casting;
                Duration = spell.Duration;
                Effect = spell.Effect;
                WeaveAdditional = spell.WeaveAdditional;
                WeaveReAttune = spell.WeaveReAttune;
                Range = spell.Range;
                AOE = spell.AOE;
                Description = spell.Description;
        }

        public string Name { get; set; }
        public string Threads { get; set; }
        public string Casting { get; set; }
        public string Duration { get; set; }
        public string Effect { get; set; }
        public string WeaveAdditional { get; set; }
        public string WeaveReAttune { get; set; }
        public string Range { get; set; }
        public string AOE { get; set; }
        public string Description { get; set; }
}