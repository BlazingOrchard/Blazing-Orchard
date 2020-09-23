using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazingOrchard.DisplayManagement.Models;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.DisplayManagement.Shapes;
using BlazingOrchard.Web.Client.Components;

namespace BlazingOrchard.Web.Client
{
    public class DemoShapeMapProvider : IShapeMapProvider
    {
        public bool GetSupportsShape(IShape shape) => ((dynamic)shape).ContentItem.ContentType == "Article";

        public ValueTask<ComponentDescriptor> DescribeComponentAsync(IShape shape, CancellationToken cancellationToken = default)
        {
            var descriptor = new ComponentDescriptor
            {
                ComponentType = typeof(Article),
                Attributes = new Dictionary<string, object>
                {
                    ["Model"] = shape
                }
            };
            
            return new ValueTask<ComponentDescriptor>(descriptor);
        }
    }
}