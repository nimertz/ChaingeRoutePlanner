using System.Text.Json.Serialization;

namespace ChaingeRoutePlanner.Models.DTO
{
    public class ShipmentDTO
    {
        public int Id { get; set; }
        
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}