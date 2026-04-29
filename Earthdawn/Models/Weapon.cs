using System;
using System.Collections.Generic;
using System.Text;

namespace EarthDawn_Assistant.src
{
    public class Weapon
    {
        // General Information
        public string Availability { get; set; }
        public string DamageStep { get; set; }
        public string MinStr { get; set; }
        public string MinDex { get; set; }
        public string Size { get; set; }

        // Range (can be null if not applicable)
        public string ShortRange { get; set; }
        public string LongRange { get; set; }

        // Core Stats
        public string Cost { get; set; }
        public string Weight { get; set; }

        // Transformed Boolean fields
        public bool Entangle { get; set; }
        public bool TwoHanded { get; set; }

        // Descriptive Text
        public string Description { get; set; }
    }
}
