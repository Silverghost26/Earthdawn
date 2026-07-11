namespace Earthdawn.Models
{
    public class Equipment
    {
        // Properties matching the JSON structure
        public string Name { get; set; } = string.Empty;
        public string type { get; set; } = string.Empty;
        public string magical { get; set; } = string.Empty;
        public string bloodCharm { get; set; } = string.Empty;
        public string Availability { get; set; } = string.Empty;
        public string Cost { get; set; } = string.Empty;
        public string Weight { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
