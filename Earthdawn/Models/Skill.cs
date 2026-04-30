using System;
using System.Collections.Generic;
using System.Text;

namespace EarthDawn.Models
{
    public class Skill
    {
        // Step is a formula string (e.g., "Rank+CHA")
        public string Step { get; set; }

        // Strain is an integer representing energy cost.
        public int Strain { get; set; }

        // Action describes when the skill is used (e.g., "Sustained").
        public string Action { get; set; }

        // Cost is a resource tier (e.g., "Novice").
        public string Cost { get; set; }

        // Description holds the lengthy text rules for the skill.
        public string Description { get; set; }
    }
}
