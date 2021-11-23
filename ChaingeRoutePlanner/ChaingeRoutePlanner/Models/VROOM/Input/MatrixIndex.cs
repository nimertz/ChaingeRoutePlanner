using System;
using System.Text.Json.Serialization;
using ChaingeRoutePlanner.Converters;
using Microsoft.EntityFrameworkCore;

namespace ChaingeRoutePlanner.Models.VROOM.Input
{
    [JsonConverter(typeof(MatrixIndexConverter))]
    [Owned]
    public class MatrixIndex
    {
        public int Row { get; set; }
        public int Column { get; set; }

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