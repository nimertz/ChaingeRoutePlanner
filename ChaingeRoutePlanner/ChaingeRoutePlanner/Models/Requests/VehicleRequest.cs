using System.Text.Json.Serialization;
using ChaingeRoutePlanner.Models.VROOM.Input;

namespace ChaingeRoutePlanner.Models.Requests
{
    public class VehicleRequest
    {
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        
        [JsonPropertyName("capacity")]
        public int? Capacity { get; set; } = 180;
        
        [JsonPropertyName("time_window")]
        public TimeWindow? TimeWindow { get; set; }
    }
}