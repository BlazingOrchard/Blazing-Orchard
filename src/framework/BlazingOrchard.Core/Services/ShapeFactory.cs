using System.Threading.Tasks;
using BlazingOrchard.Abstractions.Models;
using BlazingOrchard.Abstractions.Services;

namespace BlazingOrchard.Core.Services
{
    public class ShapeFactory : IShapeFactory
    {
        public ValueTask<Shape> CreateAsync(string shapeType)
        {
            var shape = new Shape { Metadata = { Type = shapeType } };
            return new ValueTask<Shape>(shape);
        }
    }
}