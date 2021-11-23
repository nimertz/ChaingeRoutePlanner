using System.Text.Json.Serialization;

namespace ChaingeRoutePlanner.Models.DTO
{
    public class VehicleDTO
    {
        public int Id { get; set; }
        
        [JsonPropertyName("description")]
        public string Description { get; set; }
        
        [JsonPropertyName("capacity")]
        public int Capacity { get; set; }
        
    }
}