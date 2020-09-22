using System.Threading.Tasks;
using BlazingOrchard.Contents.Services;
using BlazingOrchard.DisplayManagement.Models;
using BlazingOrchard.DisplayManagement.Services;
using OrchardCore.Client.Models;

namespace BlazingOrchard.Web.Client.Drivers
{
    public class ArticleDriver : IContentDisplayDriver
    {
        public ValueTask<IDisplayResult> BuildDisplayAsync(ContentItem model, BuildDisplayContext context)
        {
            var result = new ShapeResult("Article", shape => shape.New.Article());
            return new ValueTask<IDisplayResult>(result);
        }
    }
}