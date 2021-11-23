using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ChaingeRoutePlanner.Converters;
using ChaingeRoutePlanner.Models.VROOM.Input;

namespace ChaingeRoutePlanner.Models.Requests
{
    public class ShipmentRequest
    {
        [JsonPropertyName("pickup")]
        public bool Pickup { get; set; } = false;
        
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        
        [JsonPropertyName("location")]
        public Coordinate Location { get; set; }
        
        [JsonPropertyName("time_windows")]
        public List<TimeWindow>? TimeWindows { get; set; }
        
        [JsonPropertyName("service")]
        [JsonConverter(typeof(NullableTimeSpanSecondsToIntConverter))]
        public TimeSpan? Service { get; set; }

    }
}