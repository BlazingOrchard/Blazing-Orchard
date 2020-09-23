using System.Threading.Tasks;
using BlazingOrchard.Contents.Models;
using BlazingOrchard.DisplayManagement.Models;

namespace BlazingOrchard.Contents.Display.Services
{
    public interface IContentDisplayHandler
    {
        ValueTask BuildDisplayAsync(ContentItem contentItem, BuildDisplayContext context);
    }
}