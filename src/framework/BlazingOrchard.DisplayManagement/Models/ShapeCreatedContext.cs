using System;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.DisplayManagement.Shapes;

namespace BlazingOrchard.DisplayManagement.Models
{
    public class ShapeCreatedContext
    {
        public ShapeCreatedContext(IServiceProvider serviceProvider, IShapeFactory shapeFactory, dynamic @new, string shapeType, IShape shape)
        {
            ServiceProvider = serviceProvider;
            ShapeFactory = shapeFactory;
            New = @new;
            ShapeType = shapeType;
            Shape = shape;
        }

        public IServiceProvider ServiceProvider { get; }
        public IShapeFactory ShapeFactory { get; }
        public dynamic New { get; }
        public string ShapeType { get; }
        public IShape Shape { get; }
    }
}