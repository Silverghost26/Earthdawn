using System;

namespace Earthdawn.Models
{
    public class LanguageSkill
    {
        public LanguageSkill()
        {
        }

        public LanguageSkill(LanguageSkill languageSkill)
        {
            Language = languageSkill.Language;
            Race = languageSkill.Race;
            Description = languageSkill.Description;
            Continent = languageSkill.Continent;
        }

        public string Language { get; set; }
        public string Race { get; set; }
        public string Description { get; set; }
        public string Continent { get; set; }
        public bool ReadWrite { get; set; }
    }
}