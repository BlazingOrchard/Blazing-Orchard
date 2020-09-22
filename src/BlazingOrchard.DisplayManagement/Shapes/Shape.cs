using System.Collections.Generic;

namespace BlazingOrchard.DisplayManagement.Shapes
{
    public class Shape : Composite, IShape
    {
        private List<Shape> _items = new List<Shape>();
        public ShapeMetadata Metadata { get; set; } = new ShapeMetadata();
        public IEnumerable<dynamic> Items => _items;
        public bool HasItems => _items.Count > 0;

        public virtual Shape Add(object item)
        {
            var shape = item as Shape;
            
            if (shape != null) 
                _items.Add(shape);

            return this;
        }

        public Shape AddRange(IEnumerable<object> items)
        {
            foreach (var item in items) Add(item);

            return this;
        }

        public void Remove(string shapeName)
        {
            for (var i = _items.Count - 1; i >= 0; i--)
            {
                if (_items[i] is Shape shape && shape.Metadata.Name == shapeName)
                {
                    _items.RemoveAt(i);
                    return;
                }
            }
        }
    }
}