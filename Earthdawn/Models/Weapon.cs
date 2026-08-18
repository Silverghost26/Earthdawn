using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Earthdawn.Models;

namespace EarthDawn.Models
{
    public class Weapon : EquipmentBase
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
            Name = weapon.Name;
        }

        // General Information

        public string DamageStep { get; set; }
        public string MinStr { get; set; }
        public string MinDex { get; set; }
        public string Size { get; set; }

        // Range (can be null if not applicable)
        public string ShortRange { get; set; }
        public string LongRange { get; set; }
        
        // Transformed Boolean fields
        public bool Entangle { get; set; }
        public bool TwoHanded { get; set; }
    }
}
