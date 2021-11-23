using System;
using System.Text.Json.Serialization;
using ChaingeRoutePlanner.Converters;
using Microsoft.EntityFrameworkCore;

namespace ChaingeRoutePlanner.Models.VROOM.Input
{
    [JsonConverter(typeof(TimeWindowConverter))]
    [Owned]
    public class TimeWindow
    {
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }

        public TimeWindow(DateTimeOffset start, DateTimeOffset end)
        {
            if (start.ToUnixTimeSeconds() > end.ToUnixTimeSeconds())
            {
                throw new ArgumentException("Start must be before the end time.");
            }
            
            Start = start;
            End = end;
        }
    }
}