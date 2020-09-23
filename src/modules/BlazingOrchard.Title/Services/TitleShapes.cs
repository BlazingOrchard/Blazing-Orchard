using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazingOrchard.DisplayManagement.Models;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.DisplayManagement.Shapes;
using BlazingOrchard.Title.Components;

namespace BlazingOrchard.Title.Services
{
    public class TitleShapes : IShapeMapProvider
    {
        public bool GetSupportsShape(IShape shape) => shape.Metadata.Type == "TitlePart";

        public ValueTask<ComponentDescriptor> DescribeComponentAsync(IShape shape, CancellationToken cancellationToken = default)
        {
            var descriptor = new ComponentDescriptor
            {
                ComponentType = typeof(TitlePart),
                Attributes = new Dictionary<string, object>
                {
                    ["Model"] = shape
                }
            };
            
            return new ValueTask<ComponentDescriptor>(descriptor);
        }
    }
}