using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ChaingeRoutePlanner.Models.VROOM.Output
{
    [Owned]
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