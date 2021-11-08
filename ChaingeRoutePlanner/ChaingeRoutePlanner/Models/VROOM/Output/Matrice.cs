using System.Text.Json.Serialization;

namespace ChaingeRoutePlanner.Models.VROOM.Output
{
    public class Matrice
    {
        [JsonPropertyName("durations")]
        public int[][] Durations { get; set; } 
    }
}