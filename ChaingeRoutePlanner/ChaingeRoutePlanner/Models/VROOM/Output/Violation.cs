using System.Text.Json.Serialization;

namespace ChaingeRoutePlanner.Models.VROOM.Output
{
    public class Violation
    {
        /// <summary>
        /// The cause of the violation.
        /// </summary>
        [JsonPropertyName("cause")]
        public ViolationCause Cause { get; set; }
        
        [JsonPropertyName("duration")]
        public double Duration { get; set; }
    }
}