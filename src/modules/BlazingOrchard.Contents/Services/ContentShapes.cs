using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazingOrchard.Contents.Components;
using BlazingOrchard.DisplayManagement.Models;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.DisplayManagement.Shapes;

namespace BlazingOrchard.Contents.Services
{
    public class ContentShapes : IShapeMapProvider
    {
        public bool GetSupportsShape(IShape shape) => shape.Metadata.Type == "Content";

        public ValueTask<ComponentDescriptor> DescribeComponentAsync(IShape shape, CancellationToken cancellationToken = default)
        {
            var descriptor = new ComponentDescriptor
            {
                ComponentType = typeof(ContentShape),
                Attributes = new Dictionary<string, object>
                {
                    ["Model"] = shape
                }
            };
            return new ValueTask<ComponentDescriptor>(descriptor);
        }
    }
}