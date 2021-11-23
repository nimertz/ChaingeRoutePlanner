using System.Text.Json.Serialization;

namespace ChaingeRoutePlanner.Models.Requests
{
    public class VehicleRequest
    {
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        
        [JsonPropertyName("capacity")]
        public int? Capacity { get; set; } = 180;
    }
}