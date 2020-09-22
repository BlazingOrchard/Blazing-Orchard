using System.Collections.Generic;
using System.Threading.Tasks;
using BlazingOrchard.Contents.Services;
using BlazingOrchard.DisplayManagement.Models;
using OrchardCore.Client.Models;

namespace BlazingOrchard.Contents.Display
{
    public class ContentItemDisplayCoordinator : IContentDisplayHandler
    {
        private readonly IEnumerable<IContentDisplayDriver> _contentDisplayDrivers;
        private readonly IEnumerable<IContentPartDisplayDriver> _contentPartDisplayDrivers;

        public ContentItemDisplayCoordinator(
            IEnumerable<IContentDisplayDriver> contentDisplayDrivers,
            IEnumerable<IContentPartDisplayDriver> contentPartDisplayDrivers)
        {
            _contentDisplayDrivers = contentDisplayDrivers;
            _contentPartDisplayDrivers = contentPartDisplayDrivers;
        }

        public async ValueTask BuildDisplayAsync(ContentItem contentItem, BuildDisplayContext context)
        {
            await BuildContentDisplaysAsync(contentItem, context);
            await BuildContentPartsDisplaysAsync(contentItem, context);
        }

        private async ValueTask BuildContentDisplaysAsync(ContentItem contentItem, BuildDisplayContext context)
        {
            foreach (var driver in _contentDisplayDrivers)
            {
                var result = await driver.BuildDisplayAsync(contentItem, context);
                
                if (result != null) 
                    await result.ApplyAsync(context);
            }
        }
        
        private async Task BuildContentPartsDisplaysAsync(ContentItem contentItem, BuildDisplayContext context)
        {
            
        }
    }
}