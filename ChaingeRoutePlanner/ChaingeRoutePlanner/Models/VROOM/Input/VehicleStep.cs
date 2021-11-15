﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ChaingeRoutePlanner.Converters;
using ChaingeRoutePlanner.Models.VROOM.Output;
using Microsoft.EntityFrameworkCore;

namespace ChaingeRoutePlanner.Models.VROOM.Input
{
    [Owned]
    public class VehicleStep
    {
        /// <summary>
        /// The vehicle step type.
        /// </summary>
        [JsonPropertyName("type")]
        public StepType Type { get; set; }
        
        /// <summary>
        /// The id of the task to be performed at this step.
        /// </summary>
        [JsonPropertyName("id")]
        [Key]
        public uint? TaskId { get; set; }
        
        /// <summary>
        /// Hard constraint on service time.
        /// </summary>
        [JsonPropertyName("service_at")]
        [JsonConverter(typeof(NullableDateTimeOffsetToUnixConverter))]
        public DateTimeOffset? ServiceAt { get; set; }
        
        /// <summary>
        /// Hard constraint on service time lower bound.
        /// </summary>
        [JsonPropertyName("service_after")]
        [JsonConverter(typeof(NullableDateTimeOffsetToUnixConverter))]
        public DateTimeOffset? ServiceAfter { get; set; }
        
        /// <summary>
        /// Hard constraint on service time upper bound.
        /// </summary>
        [JsonPropertyName("service_before")]
        [JsonConverter(typeof(NullableDateTimeOffsetToUnixConverter))]
        public DateTimeOffset? ServiceBefore { get; set; }
    }
}