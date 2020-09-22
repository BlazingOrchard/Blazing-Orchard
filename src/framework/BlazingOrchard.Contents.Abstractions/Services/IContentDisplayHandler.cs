using System.Threading.Tasks;
using BlazingOrchard.DisplayManagement.Models;
using OrchardCore.Client.Models;

namespace BlazingOrchard.Contents.Services
{
    public interface IContentDisplayHandler
    {
        ValueTask BuildDisplayAsync(ContentItem contentItem, BuildDisplayContext context);
    }
}