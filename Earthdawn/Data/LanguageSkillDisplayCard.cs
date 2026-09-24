using Earthdawn.Models;

namespace Earthdawn.Data
{
    public class LanguageSkillDisplayCard
    {
        public string Name
        {
            get => _name ?? string.Empty;
            set
            {
                _name = value;
            }
        }
        private string? _name;

        public LanguageSkill LanguageSkill
        {
            get => _languageSkill ?? new LanguageSkill();
            set
            {
                _languageSkill = value;
                if (_languageSkill != null && _name != null)
                {
                    // LanguageSkill uses Language property instead of Name
                    _languageSkill.Language = _name;
                }
            }
        }
        private LanguageSkill? _languageSkill;
    }
}