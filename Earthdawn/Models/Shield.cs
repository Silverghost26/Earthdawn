using System;
using System.Collections.Generic;
using System.Text;

namespace  Earthdawn.Models
{
    public class Shield
    {
        public string Living { get; set; }
        public string Availability { get; set; }
        public int PhysicalDefense { get; set; }
        public int MysticDefense { get; set; }
        public int InitiativePenalty { get; set; }
        public int ShatterThreshold { get; set; }
        public string Cost { get; set; }
        public int Weight { get; set; }
        public string Description { get; set; }
    }
}
