using System.Text.Json.Serialization;
using ChaingeRoutePlanner.Converters;
using Microsoft.EntityFrameworkCore;

namespace ChaingeRoutePlanner.Models.VROOM.Input
{
    [JsonConverter(typeof(CoordinateConverter))]
    [Owned]
    public class Coordinate
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public Coordinate(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}