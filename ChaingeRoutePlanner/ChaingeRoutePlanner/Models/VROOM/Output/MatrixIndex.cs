using System;
using System.Text.Json.Serialization;
using ChaingeRoutePlanner.Converters;

namespace ChaingeRoutePlanner.Models.VROOM.Output
{
    [JsonConverter(typeof(MatrixIndexConverter))]
    public readonly struct MatrixIndex
    {
        public int Row { get; }
        public int Column { get; }

        public MatrixIndex(int row, int column)
        {
            if (row < 0)
            {
                throw new ArgumentException("Must be greater than 0.", nameof(row));
            }
            
            if (column < 0)
            {
                throw new ArgumentException("Must be greater than 0.", nameof(row));
            }
            
            Row = row;
            Column = column;
        }
    }
}