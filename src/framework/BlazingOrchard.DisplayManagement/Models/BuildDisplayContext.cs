using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.DisplayManagement.Shapes;

namespace BlazingOrchard.DisplayManagement.Models
{
    public class BuildDisplayContext : BuildShapeContext
    {
        public BuildDisplayContext(BuildDisplayContext context) : base(context.Shape, context.ShapeFactory)
        {
            DisplayType = context.DisplayType;
        }

        public BuildDisplayContext(IShape shape, IShapeFactory shapeFactory, string displayType) : base(
            shape,
            shapeFactory) => DisplayType = displayType;

        public string DisplayType { get; }
    }
}