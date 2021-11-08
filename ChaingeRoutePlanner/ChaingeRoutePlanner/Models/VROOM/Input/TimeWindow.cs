﻿using System;
using System.Text.Json.Serialization;
using ChaingeRoutePlanner.Converters;

namespace ChaingeRoutePlanner.Models.VROOM.Input
{
    [JsonConverter(typeof(TimeWindowConverter))]
    public readonly struct TimeWindow
    {
        public DateTimeOffset Start { get; }
        public DateTimeOffset End { get; }

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