using System;
using System.Collections.Generic;
using System.Text;

namespace Earthdawn.Models
{
    public class ArmorCollection : Dictionary<string, Armor>
    {

    }
    public class Armor : EquipmentBase
    {
        public Armor()
        {
        }

        public Armor(Armor armor)
        {
            Living = armor.Living;
            Availability = armor.Availability;
            PhysicalArmor = armor.PhysicalArmor;
            MysticArmor = armor.MysticArmor;
            InitiativePenalty = armor.InitiativePenalty;
            Cost = armor.Cost;
            Weight = armor.Weight;
            Description = armor.Description;
            Name = armor.Name;
        }
        public string Living { get; set; } = string.Empty;
        public int PhysicalArmor { get; set; } = 0;
        public int MysticArmor { get; set; } = 0;
        public int InitiativePenalty {  get; set; }
    }
}
