using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazingOrchard.DisplayManagement.Models;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.DisplayManagement.Shapes;
using BlazingOrchard.Html.Components;

namespace BlazingOrchard.Html.Services
{
    public class HtmlShapes : IShapeMapRule
    {
        public bool Matches(IShape shape) => shape.Metadata.Type == "HtmlBodyPart";

        public ValueTask<ComponentDescriptor> DescribeComponentAsync(
            IShape shape,
            CancellationToken cancellationToken = default)
        {
            var descriptor = new ComponentDescriptor
            {
                ComponentType = typeof(HtmlBodyPart),
                Attributes = new Dictionary<string, object>
                {
                    ["Model"] = shape
                }
            };

            return new ValueTask<ComponentDescriptor>(descriptor);
        }
    }
}