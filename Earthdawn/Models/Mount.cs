using System;
using System.Collections.Generic;
using System.Text;

namespace EarthDawn.Models
{
    public class Mount
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

        public string Availability { get; set; }
        public string Cost { get; set; }
        public string Weight { get; set; }
        public string CarryingCapacity { get; set; }
        public string Speed { get; set; }
        public string Lifespan { get; set; }
        public string FeedCost { get; set; }
        public string StableCost { get; set; }
        public string Description { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
