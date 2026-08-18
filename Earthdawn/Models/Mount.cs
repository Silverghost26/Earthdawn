using System;
using System.Collections.Generic;
using System.Text;
using Earthdawn.Models;

namespace EarthDawn.Models
{
    public class Mount : EquipmentBase
    {
        public Mount()
        {
        }

        public Mount(Mount mount)
        {
            Availability = mount.Availability;
            Cost = mount.Cost;
            Weight = mount.Weight;
            CarryingCapacity = mount.CarryingCapacity;
            Speed = mount.Speed;
            Lifespan = mount.Lifespan;
            FeedCost = mount.FeedCost;
            StableCost = mount.StableCost;
            Description = mount.Description;
            Name = mount.Name;
        }
        
        public string CarryingCapacity { get; set; }
        public string Speed { get; set; }
        public string Lifespan { get; set; }
        public string FeedCost { get; set; }
        public string StableCost { get; set; }
    }
}
