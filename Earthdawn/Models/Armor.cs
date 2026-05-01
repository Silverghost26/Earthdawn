using System;
using System.Collections.Generic;
using System.Text;

namespace EarthDawn.Models
{
    public class ArmorCollection : Dictionary<string, Armor>
    {

    }
    public class Armor
    {
        public string Living { get; set; } = string.Empty;
        public string Availability { get; set; } = string.Empty;
        public int PhysicalArmor { get; set; } = 0;
        public int MysticArmor { get; set; } = 0;
        public int InitiativePenalty {  get; set; }
        public string Cost {  get; set; } = string.Empty;
        public string Weight {  get; set; } = string.Empty;
        public string Description { get; set; } =  string.Empty;
    }
}
