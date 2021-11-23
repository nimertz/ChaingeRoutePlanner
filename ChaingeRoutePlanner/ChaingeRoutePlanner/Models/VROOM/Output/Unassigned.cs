using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ChaingeRoutePlanner.Models.VROOM.Input;

namespace ChaingeRoutePlanner.Models.VROOM.Output
{
    public class Unassigned
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        
        [JsonPropertyName("location")]
        public Coordinate? Location { get; set; }
    }
}