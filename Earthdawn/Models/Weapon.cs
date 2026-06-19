using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EarthDawn.Models
{
    public class Weapon
    {
        public Weapon()
        {
        }

        public Weapon(Weapon weapon)
        {
            Availability = weapon.Availability;
            DamageStep = weapon.DamageStep;
            MinDex = weapon.MinDex;
            MinStr = weapon.MinStr;
            Size = weapon.Size;
            ShortRange = weapon.ShortRange;
            LongRange = weapon.LongRange;
            Cost = weapon.Cost;
            Weight = weapon.Weight;
            Entangle = weapon.Entangle;
            TwoHanded = weapon.TwoHanded;
            Description = weapon.Description;
            WeaponName = weapon.WeaponName;
        }

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
        
        public string WeaponName { get; set; }
    }
}
