using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.DisplayManagement.Shapes;

namespace BlazingOrchard.DisplayManagement.Models
{
    public abstract class BuildShapeContext : IBuildShapeContext
    {
        protected BuildShapeContext(IShape shape, IShapeFactory shapeFactory)
        {
            Shape = shape;
            ShapeFactory = shapeFactory;
        }

        public IShape Shape { get; private set; }
        public IShapeFactory ShapeFactory { get; private set; }
        public dynamic New => ShapeFactory;
    }
}