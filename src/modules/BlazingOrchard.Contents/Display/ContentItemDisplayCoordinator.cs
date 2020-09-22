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

        public ContentItemDisplayCoordinator(IEnumerable<IContentDisplayDriver> contentDisplayDrivers)
        {
            _contentDisplayDrivers = contentDisplayDrivers;
        }

        public async Task BuildDisplayAsync(ContentItem contentItem, BuildDisplayContext context)
        {
            foreach (var driver in _contentDisplayDrivers)
            {
                var result = await driver.BuildDisplayAsync(contentItem, context);
                
                if (result != null) 
                    await result.ApplyAsync(context);
            }
        }
    }
}