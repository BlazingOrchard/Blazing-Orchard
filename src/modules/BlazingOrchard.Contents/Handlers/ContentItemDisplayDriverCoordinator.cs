using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazingOrchard.Contents.Display.Models;
using BlazingOrchard.Contents.Display.Services;
using BlazingOrchard.Contents.Models;
using BlazingOrchard.Contents.Services;
using BlazingOrchard.DisplayManagement.Models;
using BlazingOrchard.DisplayManagement.Services;

namespace BlazingOrchard.Contents.Handlers
{
    public class ContentItemDisplayDriverCoordinator : IContentDisplayHandler
    {
        private readonly IContentTypeProvider _contentTypeProvider;
        private readonly IEnumerable<ContentPart> _contentParts;
        private readonly IEnumerable<ContentField> _contentFields;
        private readonly IEnumerable<IContentDisplayDriver> _contentDisplayDrivers;
        private readonly IEnumerable<IContentPartDisplayDriver> _contentPartDisplayDrivers;
        private readonly IEnumerable<IContentFieldDisplayDriver> _contentFieldDisplayDrivers;

        public ContentItemDisplayDriverCoordinator(
            IContentTypeProvider contentTypeProvider,
            IEnumerable<ContentPart> contentParts,
            IEnumerable<IContentDisplayDriver> contentDisplayDrivers,
            IEnumerable<IContentPartDisplayDriver> contentPartDisplayDrivers,
            IEnumerable<IContentFieldDisplayDriver> contentFieldDisplayDrivers,
            IEnumerable<ContentField> contentFields)
        {
            _contentTypeProvider = contentTypeProvider;
            _contentParts = contentParts;
            _contentDisplayDrivers = contentDisplayDrivers;
            _contentPartDisplayDrivers = contentPartDisplayDrivers;
            _contentFieldDisplayDrivers = contentFieldDisplayDrivers;
            _contentFields = contentFields;
        }

        public async ValueTask BuildDisplayAsync(ContentItem contentItem, BuildDisplayContext context)
        {
            var contentTypeDescriptor = await _contentTypeProvider.GetAsync(contentItem.ContentType);
            await BuildContentDisplaysAsync(contentItem, context);
            await BuildContentPartsDisplaysAsync(contentItem, contentTypeDescriptor, context);
        }

        private async ValueTask BuildContentDisplaysAsync(ContentItem contentItem, BuildDisplayContext context)
        {
            foreach (var driver in _contentDisplayDrivers)
            {
                var result = await driver.BuildDisplayAsync(contentItem, context);
                await ApplyDisplayResultAsync(result, context);
            }
        }

        private async Task BuildContentPartsDisplaysAsync(
            ContentItem contentItem,
            ContentTypeDescriptor contentTypeDescriptor,
            BuildDisplayContext context)
        {
            foreach (var contentTypePartDescriptor in contentTypeDescriptor.Parts)
                await BuildContentPartDisplaysAsync(contentItem, contentTypePartDescriptor, context);
        }

        private async Task BuildContentPartDisplaysAsync(
            ContentItem contentItem,
            ContentTypePartDescriptor contentTypePartDescriptor,
            BuildDisplayContext context)
        {
            var partName = contentTypePartDescriptor.Name;
            var partTypeName = contentTypePartDescriptor.Part.Name;
            var partType = _contentParts.FirstOrDefault(x => x.GetType().Name == partTypeName)?.GetType() ?? typeof(ContentPart);

            if (partType == null)
                return;

            if (!(contentItem.Get(partType, partName) is ContentPart contentPart))
                return;

            var drivers = _contentPartDisplayDrivers.Where(x => x.PartType == partType);
            var partDisplayContext = new BuildPartDisplayContext(contentTypePartDescriptor, context);

            foreach (var driver in drivers)
            {
                var result = await driver.BuildDisplayAsync(contentPart, partDisplayContext);
                await ApplyDisplayResultAsync(result, context);
            }

            await BuildContentFieldsDisplaysAsync(contentItem, contentPart, contentTypePartDescriptor, context);
        }

        private async ValueTask BuildContentFieldsDisplaysAsync(
            ContentItem contentItem,
            ContentPart contentPart,
            ContentTypePartDescriptor contentTypePartDescriptor,
            BuildDisplayContext context)
        {
            foreach (var contentPartFieldDescriptor in contentTypePartDescriptor.Part.Fields)
                await BuildContentFieldDisplaysAsync(
                    contentItem,
                    contentPart,
                    contentTypePartDescriptor,
                    contentPartFieldDescriptor,
                    context);
        }

        private async ValueTask BuildContentFieldDisplaysAsync(
            ContentItem contentItem,
            ContentPart contentPart,
            ContentTypePartDescriptor contentTypePartDescriptor,
            ContentPartFieldDescriptor contentPartFieldDescriptor,
            BuildDisplayContext context)
        {
            var fieldName = contentPartFieldDescriptor.Name;
            var fieldTypeName = contentPartFieldDescriptor.Field.Name;
            var fieldType = _contentFields.FirstOrDefault(x => x.GetType().Name == fieldTypeName)?.GetType();

            if (fieldType == null)
                return;

            if (!(contentPart.Get(fieldType, fieldName) is ContentField field))
                return;

            var drivers = _contentFieldDisplayDrivers.Where(x => x.FieldType == fieldType);
            var fieldDisplayContext = new BuildFieldDisplayContext(contentTypePartDescriptor, contentPartFieldDescriptor, context);

            foreach (var driver in drivers)
            {
                var result = await driver.BuildDisplayAsync(field, fieldDisplayContext);
                await ApplyDisplayResultAsync(result, context);
            }
        }

        private async Task ApplyDisplayResultAsync(IDisplayResult? result, BuildDisplayContext context)
        {
            if (result != null)
                await result.ApplyAsync(context);
        }
    }
}