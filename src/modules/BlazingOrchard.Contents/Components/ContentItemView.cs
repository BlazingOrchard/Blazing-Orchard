using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazingOrchard.Contents.Display.Services;
using BlazingOrchard.Contents.Models;
using BlazingOrchard.Contents.Services;
using BlazingOrchard.DisplayManagement.Models;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.DisplayManagement.Shapes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazingOrchard.Contents.Components
{
    public class ContentItemView : ComponentBase
    {
        [Parameter] public ContentItem? ContentItem { get; set; }
        [Parameter] public string? Alias { get; set; }
        [Parameter] public string? DisplayType { get; set; }
        [Inject] private IContentProvider ContentProvider { get; set; } = default!;
        [Inject] private IContentDisplayManager ContentDisplayManager { get; set; } = default!;
        [Inject] private IEnumerable<IShapeMapProvider> ShapeMapProviders { get; set; } = default!;
        private IShape? Shape { get; set; }
        private ComponentDescriptor? ComponentDescriptor { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            ComponentDescriptor = null;
            
            if (!string.IsNullOrWhiteSpace(Alias))
                ContentItem = await ContentProvider.GetByAliasAsync(Alias);

            if (ContentItem != null)
            {
                Shape = await ContentDisplayManager.BuildDisplayAsync(ContentItem, DisplayType);

                var shapeMapProvider = ShapeMapProviders.FirstOrDefault(x => x.GetSupportsShape(Shape));

                if (shapeMapProvider != null)
                {
                    ComponentDescriptor = await shapeMapProvider.DescribeComponentAsync(Shape);
                }
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            if (ComponentDescriptor == null)
                return;
            
            builder.OpenComponent(0, ComponentDescriptor.ComponentType);
            var attributeSequence = 1;
            
            foreach (var (key, value) in ComponentDescriptor.Attributes)
                builder.AddAttribute(attributeSequence++, key, value);

            builder.CloseComponent();
        }
    }
}