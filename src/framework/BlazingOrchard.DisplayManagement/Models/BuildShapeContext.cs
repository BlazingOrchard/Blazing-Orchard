using BlazingOrchard.DisplayManagement.Descriptors;
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
            FindPlacement = FindDefaultPlacement;
        }

        public IShape Shape { get; }
        public IShapeFactory ShapeFactory { get; }
        public dynamic New => ShapeFactory;
        public FindPlacementDelegate FindPlacement { get; set; }
        public string? DefaultZone { get; set; }
        public string? DefaultPosition { get; set; }

        private PlacementInfo? FindDefaultPlacement(
            string shapeType,
            string? differentiator,
            string? displayType,
            IBuildShapeContext context) => default;
    }
}