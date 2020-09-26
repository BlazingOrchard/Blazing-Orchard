using System.Collections.Generic;

namespace BlazingOrchard.DisplayManagement.Shapes
{
    public class PositionWrapper : IPositioned, IShape
    {
        private readonly string _value;
        private Dictionary<string, object>? _properties;

        public PositionWrapper(string value, string position)
        {
            _value = value;
            Position = position;
        }
        
        public string Position { get; set; }
        public ShapeMetadata Metadata { get; set; } = new ShapeMetadata();
        public IDictionary<string, object> Properties => _properties ??= new Dictionary<string, object>();
    }
}
