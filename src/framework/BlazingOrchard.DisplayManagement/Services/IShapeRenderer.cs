using BlazingOrchard.DisplayManagement.Shapes;
using Microsoft.AspNetCore.Components;

namespace BlazingOrchard.DisplayManagement.Services
{
    public interface IShapeRenderer
    {
        RenderFragment RenderShape(IShape shape);
    }
}