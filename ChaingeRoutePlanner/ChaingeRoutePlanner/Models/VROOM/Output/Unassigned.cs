using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ChaingeRoutePlanner.Models.VROOM.Input;

namespace ChaingeRoutePlanner.Models.VROOM.Output
{
    public class Unassigned
    {
        /// <summary>
        /// For EF core purposes only not included in Vroom API
        /// </summary>
        [JsonIgnore]
        [Key]
        public int UnassignedId { get; set; }
        
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        
        [JsonPropertyName("location")]
        public Coordinate? Location { get; set; }
    }
}