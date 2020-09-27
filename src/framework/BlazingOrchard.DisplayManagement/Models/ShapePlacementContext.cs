using BlazingOrchard.DisplayManagement.Shapes;

namespace BlazingOrchard.DisplayManagement.Models
{
    public class ShapePlacementContext
    {
        public ShapePlacementContext(string shapeType, string displayType, string differentiator, IShape zoneShape)
        {
            ShapeType = shapeType;
            DisplayType = displayType;
            Differentiator = differentiator;
            ZoneShape = zoneShape;
        }

        public IShape ZoneShape { get; set; }
        public string ShapeType { get; set; }
        public string DisplayType { get; set; }
        public string Differentiator { get; set; }

        /// <summary>
        /// Debug information explaining where the final placement is coming from.
        /// Used by tooling.
        /// </summary>
        public string? Source { get; set; }
    }
}