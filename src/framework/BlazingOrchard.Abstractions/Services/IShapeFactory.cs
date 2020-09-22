using System.Threading.Tasks;
using BlazingOrchard.Abstractions.Models;

namespace BlazingOrchard.Abstractions.Services
{
    public interface IShapeFactory
    {
        ValueTask<Shape> CreateAsync(string shapeType);
    }
}