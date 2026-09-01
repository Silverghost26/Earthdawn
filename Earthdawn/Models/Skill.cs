using System;
using System.Collections.Generic;
using System.Text;

namespace Earthdawn.Models
{
    public class Skill
    {
        public Skill()
        {
        }

        public Skill(Skill skill)
        {
            Step = skill.Step;
            Strain = skill.Strain;
            Action = skill.Action;
            Tier = skill.Tier;
            Description = skill.Description;
            Name = skill.Name;
            Rank = skill.Rank;
        }
    

        // Step is a formula string (e.g., "CHA")
        public string Step { get; set; }

        // Strain is an integer representing hit points of damage taken (if any) for using the skill.
        public int Strain { get; set; }

        // Action describes when the skill is used (e.g., "Sustained").
        public string Action { get; set; }

        // Tier required of the skill (e.g., "Novice").
        public string Tier { get; set; }

        // Description holds the lengthy text rules for the skill.
        public string Description { get; set; }
        
        public string Name { get; set; }
        public int Rank { get; set; }
    }
}
