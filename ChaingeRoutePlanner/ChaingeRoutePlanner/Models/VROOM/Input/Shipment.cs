using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ChaingeRoutePlanner.Models.VROOM.Input
{
    public class Shipment
    {
        /// <summary>
        /// For EF core purposes only not included in Vroom API
        /// </summary>
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        
        [JsonIgnore]
        [ForeignKey("Pickup")]
        public int PickupId { get; set; }
        
        /// <summary>
        /// A ShipmentStep object describing pickup.
        /// </summary>
        [JsonPropertyName("pickup")]
        public ShipmentStep Pickup { get; set; }
        
        [JsonIgnore]
        [ForeignKey("Delivery")]
        public int DeliveryId { get; set; }
        
        /// <summary>
        /// A ShipmentStep object describing delivery.
        /// </summary>
        [JsonPropertyName("delivery")]
        public ShipmentStep Delivery { get; set; }
        
        /// <summary>
        /// List of ints describing multidimensional quantities for delivery.
        /// </summary>
        [JsonPropertyName("amount")]
        public List<int>? Amount { get; set; }
        
        /// <summary>
        /// A List of ints defining mandatory skills.
        /// </summary>
        [JsonPropertyName("skills")]
        public List<int>? Skills { get; set; }
        
        /// <summary>
        /// An integer in the [0, 100] range describing priority level.
        /// </summary>
        [JsonPropertyName("priority")]
        public Priority? Priority { get; set; }
    }
}