using BlazingOrchard.DisplayManagement.Shapes;

namespace BlazingOrchard.DisplayManagement.Services
{
    /// <summary>
    /// Represents a context object that is used to build and place a list of shape representing
    /// something that has to be displayed.
    /// </summary>
    public interface IBuildShapeContext
    {
        IShape Shape { get; }
        IShapeFactory ShapeFactory { get; }
        dynamic New { get; }
    }
}