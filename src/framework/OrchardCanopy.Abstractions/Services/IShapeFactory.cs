using System.Threading.Tasks;
using OrchardCanopy.Abstractions.Models;

namespace OrchardCanopy.Abstractions.Services
{
    public interface IShapeFactory
    {
        ValueTask<Shape> CreateAsync(string shapeType);
    }
}