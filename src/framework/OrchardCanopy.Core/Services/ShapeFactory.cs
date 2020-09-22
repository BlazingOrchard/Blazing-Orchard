using System.Threading.Tasks;
using OrchardCanopy.Abstractions.Models;
using OrchardCanopy.Abstractions.Services;

namespace OrchardCanopy.Core.Services
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