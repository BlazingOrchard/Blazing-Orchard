using System;
using System.Threading.Tasks;
using BlazingOrchard.DisplayManagement.Shapes;

namespace BlazingOrchard.DisplayManagement.Services
{
    public abstract class DisplayDriverBase
    {
        protected ShapeResult Shape(string shapeType, Func<IBuildShapeContext, ValueTask<IShape?>> shapeBuilder) =>
            new ShapeResult(shapeType, shapeBuilder);
    }
}