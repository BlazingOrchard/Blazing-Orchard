using Microsoft.AspNetCore.Components;

namespace BlazingOrchard.DisplayManagement.Components
{
    public class ShapeTemplate : ComponentBase
    {
        [Parameter] public dynamic Model { get; set; } = default!;
    }
}