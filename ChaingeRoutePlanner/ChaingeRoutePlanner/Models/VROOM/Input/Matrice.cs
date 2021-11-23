using System.Text.Json.Serialization;

namespace ChaingeRoutePlanner.Models.VROOM.Input
{
    public class Matrice
    {
        [JsonPropertyName("durations")]
        public int[][] Durations { get; set; } 
    }
}