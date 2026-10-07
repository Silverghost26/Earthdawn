using System;

namespace Earthdawn.Models
{
    public class 
        KnowledgeSkill
    {
        public KnowledgeSkill()
        {
        }

        public KnowledgeSkill(KnowledgeSkill knowledgeSkill)
        {
            Name = knowledgeSkill.Name;
            Description = knowledgeSkill.Description;
            Rank = knowledgeSkill.Rank;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Rank { get; set; }
    }
}