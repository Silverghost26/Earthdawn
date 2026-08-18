namespace Earthdawn.Models
{
    public class Equipment : EquipmentBase
    {
        // Properties matching the JSON structure
        public string type { get; set; } = string.Empty;
        public string magical { get; set; } = string.Empty;
        public string bloodCharm { get; set; } = string.Empty;
        
        
    }
}
