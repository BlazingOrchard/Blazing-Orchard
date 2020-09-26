namespace BlazingOrchard.DisplayManagement.Shapes
{
    public class ShapeMetadata
    {
        public string Type { get; set; } = default!;
        public string? DisplayType { get; set; }
        public string? Name { get; set; }
        public string? Differentiator { get; set; }
        public string? PlacementSource { get; set; }
    }
}