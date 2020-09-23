using System.Threading.Tasks;
using BlazingOrchard.Contents.Models;
using BlazingOrchard.DisplayManagement.Shapes;

namespace BlazingOrchard.Contents.Display.Services
{
    public interface IContentDisplayManager
    {
        ValueTask<IShape> BuildDisplayAsync(ContentItem contentItem, string? displayType = default);
    }
}