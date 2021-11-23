using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ChaingeRoutePlanner.Models.VROOM.Output
{
    public class VroomOutput
    {
        /// <summary>
        /// For EF core purposes only not included in Vroom API
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        [Key]
        public int Id { get; set; }

        [JsonIgnore]
        public bool WasSuccessful => Code == OutputCode.NoError;
        
        /// <summary>
        /// Status code.
        /// </summary>
        [JsonPropertyName("code")]
        public OutputCode Code { get; set; }
        
        /// <summary>
        /// Error message. Present if code is different from 0.
        /// </summary>
        [JsonPropertyName("error")]
        public string? Error { get; set; }
        
        /// <summary>
        /// Object summarising solution indicators.
        /// </summary>
        [JsonPropertyName("summary")]
        public Summary Summary { get; set; }
        
        /// <summary>
        /// List of objects describing unassigned tasks with their id, type and location (if provided).
        /// </summary>
        [JsonPropertyName("unassigned")]
        public List<Unassigned>? Unassigned { get; set; }
        
        /// <summary>
        /// List of route objects.
        /// </summary>
        [JsonPropertyName("routes")]
        public List<Route> Routes { get; set; }
    }
}