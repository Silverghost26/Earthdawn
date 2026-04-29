namespace Earthdawn.Models;

public class Talent
{
     // Maps to the "Step" key
        public string Step { get; set; }

        // Maps to the "Strain" key (Since it's an integer, we use 'int')
        public int Strain { get; set; }

        // Maps to the "Action" key
        public string Action { get; set; }

        // Maps to the "SkillUse" key
        public string SkillUse { get; set; }

        // Maps to the "SkillLevel" key
        public string SkillLevel { get; set; }

        // Maps to the "Description" key
        public string Description { get; set; }
}