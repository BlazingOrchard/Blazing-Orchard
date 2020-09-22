using System.Collections.Generic;

namespace BlazingOrchard.Abstractions.Models
{
    public class Shape
    {
        public ShapeMetadata Metadata { get; set; } = new ShapeMetadata();
        public ICollection<Shape> Items { get; set; } = new List<Shape>();
    }
}