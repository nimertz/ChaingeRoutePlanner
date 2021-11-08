using System.Text.Json.Serialization;
using ChaingeRoutePlanner.Converters;

namespace ChaingeRoutePlanner.Models.VROOM.Input
{
    [JsonConverter(typeof(CoordinateConverter))]
    public readonly struct Coordinate
    {
        public double Longitude { get; }
        public double Latitude { get; }

        public Coordinate(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}