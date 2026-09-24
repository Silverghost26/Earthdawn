using Earthdawn.Models;

namespace Earthdawn.Data
{
    public class KnowledgeSkillDisplayCard
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

        public KnowledgeSkill KnowledgeSkill
        {
            get => _knowledgeSkill ?? new KnowledgeSkill();
            set
            {
                _knowledgeSkill = value;
                _knowledgeSkill.Name = _name;
            }
        }
        private KnowledgeSkill? _knowledgeSkill;
    }
}