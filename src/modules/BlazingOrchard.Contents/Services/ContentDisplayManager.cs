using System.Collections.Generic;
using System.Threading.Tasks;
using BlazingOrchard.Contents.Display.Services;
using BlazingOrchard.Contents.Models;
using BlazingOrchard.DisplayManagement.Extensions;
using BlazingOrchard.DisplayManagement.Models;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.DisplayManagement.Shapes;

namespace BlazingOrchard.Contents.Services
{
    public class ContentDisplayManager : IContentDisplayManager
    {
        private readonly IShapeFactory _shapeFactory;
        private readonly IEnumerable<IContentDisplayHandler> _contentDisplayHandlers;

        public ContentDisplayManager(IShapeFactory shapeFactory,
            IEnumerable<IContentDisplayHandler> contentDisplayHandlers)
        {
            _shapeFactory = shapeFactory;
            _contentDisplayHandlers = contentDisplayHandlers;
        }

        public async ValueTask<IShape> BuildDisplayAsync(ContentItem contentItem, string? displayType = default)
        {
            var actualShapeType = "Content";
            var actualDisplayType = string.IsNullOrEmpty(displayType) ? "Detail" : displayType;
            dynamic itemShape = await CreateContentShapeAsync(actualShapeType);

            itemShape.ContentItem = contentItem;

            ShapeMetadata metadata = itemShape.Metadata;
            metadata.DisplayType = actualDisplayType;
            metadata.Alternates.Add($"Content__{contentItem.ContentType}");
            
            var context = new BuildDisplayContext(
                itemShape,
                _shapeFactory,
                actualDisplayType
            );

            foreach (var handler in _contentDisplayHandlers) 
                await handler.BuildDisplayAsync(contentItem, context);

            return context.Shape;
        }

        protected async ValueTask<IShape> CreateContentShapeAsync(string actualShapeType) =>
            await _shapeFactory.CreateAsync(actualShapeType);
    }
}