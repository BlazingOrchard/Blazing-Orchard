using System.Threading.Tasks;
using BlazingOrchard.DisplayManagement.Models;
using BlazingOrchard.DisplayManagement.Shapes;

namespace BlazingOrchard.DisplayManagement.Services
{
    public interface IShapeFactory
    {
        ValueTask<IShape> CreateAsync(string shapeType);
    }
}