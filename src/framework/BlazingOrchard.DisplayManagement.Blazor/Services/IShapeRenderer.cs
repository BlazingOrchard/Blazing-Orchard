using BlazingOrchard.DisplayManagement.Shapes;
using Microsoft.AspNetCore.Components;

namespace BlazingOrchard.DisplayManagement.Blazor.Services
{
    public interface IShapeRenderer
    {
        RenderFragment RenderShape(IShape shape);
    }
}