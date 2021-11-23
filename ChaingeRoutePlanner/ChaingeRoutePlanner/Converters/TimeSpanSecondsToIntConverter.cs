using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChaingeRoutePlanner.Converters
{
    public class TimeSpanSecondsToIntConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new TimeSpan(0, 0, reader.GetInt32());
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue((int)Math.Round(value.TotalSeconds));
        }
    }
}